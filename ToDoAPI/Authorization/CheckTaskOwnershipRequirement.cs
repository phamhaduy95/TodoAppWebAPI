using Microsoft.AspNetCore.Authorization;
using SharedObjects.IntermidiateModel;

namespace ToDoAPI.Authorization
{
    public class CheckTaskOwnershipRequirement : IAuthorizationRequirement
    {
    }

    public class CheckTaskOwnershipHandler : AuthorizationHandler<CheckTaskOwnershipRequirement, TaskModel>
    {
        public CheckTaskOwnershipHandler()
        {
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckTaskOwnershipRequirement requirement, TaskModel model)
        {
            Guid userId;
            if (!Guid.TryParse(context.User.FindFirst(c => c.Type == "userId")?.Value, out userId))
            {
                return;
            };
            if (userId == model.UserId)
            {
                context.Succeed(requirement);
            }
            return;
        }
    }
}