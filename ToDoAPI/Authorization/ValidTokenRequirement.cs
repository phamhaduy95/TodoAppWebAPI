using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToDoAPI.Authorization
{
    public class ValidTokenRequirement : IAuthorizationRequirement
    {
        public ValidTokenRequirement()
        {
        }
    }

    public class ValidTokenHandler : AuthorizationHandler<ValidTokenRequirement>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ValidTokenHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenRequirement requirement)
        {
            Guid userId;
            if (!Guid.TryParse(context.User.FindFirst(c => c.Type == "userId")?.Value, out userId))
            {
                return;
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return;
            context.Succeed(requirement);
        }
    }
}