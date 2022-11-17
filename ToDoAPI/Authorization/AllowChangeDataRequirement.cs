using Microsoft.AspNetCore.Authorization;

namespace ToDoAPI.Authorization
{
    public class AllowChangeDataRequirement : IAuthorizationRequirement
    {
        public AllowChangeDataRequirement()
        {
        }
    }

    internal class AllowChangeDataHandler : AuthorizationHandler<AllowChangeDataRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowChangeDataRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "ChangeDataEnabled" && c.Value == true.ToString()))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}