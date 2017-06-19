using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Controllers
{
    
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger logger;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILoggerFactory loggerFactory)
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
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    logger.LogError(3, $"User {user.Email} created a new account with password.");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        #endregion

       
        #region Login HttpGet
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await signInManager.SignOutAsync(); //LogOut
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

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                

                if (result.Succeeded)
                {
                    logger.LogError(1, $"User {user?.Email} logged");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    logger.LogError(2, $"User {user?.Email}  account locked out.");
                  //  return View("Lockout");
                }
                else
                {
                    if (user==null)
                    {
                        ModelState.AddModelError("UserNotFound", "UserNotFound");
                    }
                    ModelState.AddModelError("PasswordError", "Invalid login attempt.");
                    logger.LogError(2, $"User {user?.Email}  account eroe .");
                    return View(model);
                }
            }

            return View(model);
        }
        #endregion

        #region LogOff HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff(string returnUrl = null)
        {
            // remove coocies
            await signInManager.SignOutAsync();
            logger.LogError(4, "User logged out.");
            return RedirectToLocal(returnUrl);
            // return RedirectToAction("Index", "Home");
        }
        #endregion


        #region AccessDenied HttpGet
        [AllowAnonymous]
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