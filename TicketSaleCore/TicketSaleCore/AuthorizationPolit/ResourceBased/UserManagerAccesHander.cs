using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.AuthorizationPolit.ResourceBased
{
    public class UserManagerAccesHander : AuthorizationHandler<OperationAuthorizationRequirement, UserManager<AppUser>>
    {

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            UserManager<AppUser> resource)
        {
            
               var isCanAccesed = context.User.FindFirst(c => c.Type == "UserManagerAcces" &&  c.Value == requirement.Name);
            if(isCanAccesed != null)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }

}