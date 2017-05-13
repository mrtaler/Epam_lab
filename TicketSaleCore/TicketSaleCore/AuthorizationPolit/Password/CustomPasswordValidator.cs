using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketSaleCore.Models;

namespace TicketSaleCore.AuthorizationPolit.Password
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public int RequiredLength { get; set; } // минимальная длина

        public CustomPasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync
            (UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = String.Format("Минимальная длина пароля равна {0}", RequiredLength)
                });
            }
            string pattern = "^[A-Za-z0-9]+$";

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен состоять только избукв и цифр"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}