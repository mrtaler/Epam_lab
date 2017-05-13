using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models;
using TicketSaleCore.Services;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            logger = loggerFactory.CreateLogger<AccountController>();
        }

        #region Register HttpGet




        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        #endregion
        #region Register HttpPost
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"ѕодтвердите регистрацию, перейд€ по ссылке: <a href='{callbackUrl}'>link</a>");

                    logger.LogInformation(3, $"User {user.Email} created a new account with password.");
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        #endregion

        #region ConfirmEmail HttpGet
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        #endregion
        #region Login HttpGet
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {

            await signInManager.SignOutAsync(); //remove all early logins
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        #endregion

        #region Login HttpPost
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // провер€ем, подтвержден ли email
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "¬ы не подтвердили свой email");
                        return View(model);
                    }
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    logger.LogInformation(1, $"User {user?.Email} logged");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning(2, $"User {user?.Email}  account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }
        #endregion

        #region LogOff HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удал€ем аутентификационные куки
            await signInManager.SignOutAsync();
            logger.LogInformation(4, "User logged out.");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ForgotPassword HttpGet

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        #endregion

        #region ForgotPassword HttpPost

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region ForgotPasswordConfirmation HttpGet

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region ResetPassword HttpGet

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        #endregion

        #region ResetPassword HttpPost

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        #endregion

        #region ResetPasswordConfirmation HttpGet

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region AccessDenied HttpGet

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region Magic
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion
    }
}