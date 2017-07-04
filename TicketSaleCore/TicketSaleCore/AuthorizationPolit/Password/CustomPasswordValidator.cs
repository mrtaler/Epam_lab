using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.AuthorizationPolit.Password
{
    /// <summary>
    /// Speshial pasvord Validator
    /// </summary>
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public int RequiredLength { get; set; } // Min pasword lenght 
        public CustomPasswordValidator(int length)
        {
            RequiredLength = length;
        }
        /// <summary>
        /// password validator
        /// </summary>
        /// <param name="manager">Application user manager</param>
        /// <param name="user">Check User</param>
        /// <param name="password">Password for check</param>
        /// <returns></returns>
        public Task<IdentityResult> ValidateAsync
            (UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = String.Format("The minimum password length is {0} symbols", RequiredLength)
                });
            }

            string pattern = "^[A-Za-z0-9]+$";//password pattern

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "The password should only consist of letters and numbers"//"Пароль должен состоять только избукв и цифр"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}