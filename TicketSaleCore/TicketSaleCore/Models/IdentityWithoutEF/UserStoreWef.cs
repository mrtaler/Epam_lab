using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.IdentityWithoutEF
{
    public class UserStoreWef : 
        IUserPasswordStore<AppUser>,
        IUserLoginStore<AppUser>,
        IUserRoleStore<AppUser>
        {
        
        private static readonly List<AppUser> Users = new List<AppUser>();

        public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            user.Id = Guid.NewGuid().ToString();

            Users.Add(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            var match = Users.FirstOrDefault(u => u.Id == user.Id);
            if (match != null)
            {
                match.UserName = user.UserName;
                match.Email = user.Email;
                match.PhoneNumber = user.PhoneNumber;
                match.TwoFactorEnabled = user.TwoFactorEnabled;
                match.PasswordHash = user.PasswordHash;

                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            var match = Users.FirstOrDefault(u => u.Id == user.Id);
            if (match != null)
            {
                Users.Remove(match);

                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }
        }

        public Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);

            return Task.FromResult(user);
        }

        public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = Users.FirstOrDefault(u => String.Equals(u.NormalizedUserName, normalizedUserName, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(user);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }


        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;

            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            // Do nothing. In this simple example, the normalized user name is generated from the user name.

            return Task.FromResult(true);
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult(true);
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user, CancellationToken cancellationToken)
        {
            // Just returning an empty list because I don't feel like implementing this. You should get the idea though...
            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            return Task.FromResult(logins);
        }

        public Task<AppUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(AppUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(AppUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }

        public Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            //var qq = new IdentityUserRole<string>();
            //user.Roles.Add(
               
                
            //    );
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
        {
            //List<string> roles = userRolesTable.FindByUserId(user.Id);

            //if (user.Email.Equals("Admin"))
            //{
            //    return (IList<string>)await this.UserRoles , this.Roles

            //}
            //if (user.Email.Equals("User1"))
            //{

            //}
            //if (user.Email.Equals("User2"))
            //{

            //}
            //if (user.Email.Equals("User3"))
            //{

            //}
            throw new NotImplementedException();

        }

        public async Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");

            //if (String.IsNullOrEmpty(roleName))
            //    throw new ArgumentNullException("roleName");

            //if (user == null)
            //{
            //    throw new ArgumentNullException("user");
            //}

            //if (user.UserName.Contains("Admin"))
            //{
            //    if (String.Equals(roleName, "admin", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        return await Task.FromResult<bool>(true);
            //    }
            //    else
            //    {
            //        return await Task.FromResult<bool>(false);
            //    }
            //}
            //if (user.UserName.Contains("User") )
            //{
            //    if (String.Equals(roleName, "user", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        return await Task.FromResult<bool>(true);
            //    }
            //    else
            //    {
            //        return await Task.FromResult<bool>(false);
            //    }
            //}
            //return await Task.FromResult<bool>(false);
            throw new NotImplementedException();
        }

        public Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}