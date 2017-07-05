using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.AuthorizationPolit.UserAndPassword
{
    public class MyUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();


            string userNamePattern = "^[A-Z]{1}[a-z0-9]{1,}";

            if(!Regex.IsMatch(user.Email, userNamePattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "The user name is not valid"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}

