using Microsoft.AspNetCore.Authorization;
using SharedObjects.IntermidiateModel;
using System.Security.Claims;

namespace ToDoAPI.Authorization
{
    public class CheckGuestAccountRequirement : IAuthorizationRequirement
    {
        public CheckGuestAccountRequirement()
        {
        }
    }

    internal class CheckGuestAccountHandler : AuthorizationHandler<CheckGuestAccountRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckGuestAccountRequirement requirement)
        {

            var Email = context.User.FindFirst(c => c.Type == ClaimTypes.Email && c.Value == "guest@email.com");

            if (Email == null)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, "guest account can not be modified"));
            }
              return Task.CompletedTask;
        }
    }
}