using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TicketSaleCore.AuthorizationPolit.Age
{
    public class AgeHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                var year = 0;
                if (Int32.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value, out year))
                {
                    if ((DateTime.Now.Year - year) >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
