using BussinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedObjects.BindingModel;
using SharedObjects.Common;
using SharedObjects.IntermidiateModel;

namespace ToDoAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize(Policy = "SecurityTokenCheck")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorizationService _authService;

        public TaskController(ITaskService taskService,
            IUserService userService, ICategoryService categoryService,
            IAuthorizationService authService)

        {
            _taskService = taskService;
            _userService = userService;
            _categoryService = categoryService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTask([FromBody] AddNewTaskModel model)
        {
            // since, it has to pass the userId check in ValidTokenRequirement authorization so userId is valid here.
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.CategoryId != null && model.CategoryId != default(Guid))
            {
                var category = await _categoryService.GetCategoryAsync((Guid)model.CategoryId);
                if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            }
            var taskId = Guid.NewGuid();
            var newTaskToAdd = new TaskModel
            {
                TaskId = taskId,
                Status = ToDoTaskStatus.OnGoing,
                CreatedTime = DateTimeOffset.Now,
                Description = model.Description,
                UserId = userId,
                Title = model.Title,
                EndTime = model.EndTime,
                StartTime = model.StartTime,
                CategoryId = model.CategoryId,
            };

            var response = await _taskService.CreateTaskAsync(newTaskToAdd);
            if (response.StatusCode == 200) return Ok(new { TaskId = taskId });
            return response.GenerateActionResult();
        }

        [HttpPut]
        [Route("{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] UpdateTaskModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null) return NotFound(new { TaskId = "taskId not found" });
            // check whether targeted task belongs to current user.
            var authResult = await _authService.AuthorizeAsync(User, task, "UserOwnsTaskCheck");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            };

            if (model.CategoryId != null && model.CategoryId != default(Guid))
            {
                var category = await _categoryService.GetCategoryAsync((Guid)model.CategoryId);
                if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var TaskToUpdateModel = new TaskModel
            {
                TaskId = taskId,
                Status = model.Status,
                EndTime = model.EndTime,
                Description = model.Description,
                StartTime = model.StartTime,
                Title = model.Title,
                UserId = default(Guid),
                CategoryId = model.CategoryId,
            };
            var response = await _taskService.UpdateTaskAsync(TaskToUpdateModel);
            return response.GenerateActionResult();
        }

        [HttpDelete]
        [Route("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null) return NotFound(new { TaskId = "taskId not found" });
            // check whether targeted task belongs to current user.
            var authResult = await _authService.AuthorizeAsync(User, task, "UserOwnsTaskCheck");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            };

            var response = await _taskService.DeleteTaskAsync(taskId);
            return response.GenerateActionResult();
        }

        [HttpGet]
        [Route("{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null) return NotFound(new ErrorMessage("taskId not found"));

            // check whether targeted task belongs to current user.
            var authResult = await _authService.AuthorizeAsync(User, task, "UserOwnsTaskCheck");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            };

            return Ok(task);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTasks([FromQuery] Guid? userId)
        {
            Guid id;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out id);

            if (userId == null)
            {
                var tasks = await _taskService.GetAllAsync();
                return Ok(tasks);
            }
            var user = await _userService.GetUserByIdAsync((Guid)userId);
            if (user == null) return NotFound(new { UserId = "UserId not found" });
            var tasksForUser = await _taskService.GetAllTasksForUserAsync((Guid)userId);
            return Ok(tasksForUser);
        }

        [HttpGet]
        [Route("within-a-month")]
        public async Task<IActionResult> GetTaskInMonth([FromQuery] GetTaskInMonthModel model)
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound(new { UserId = "UserId not found" });
            try
            {
                var firstDateOfMonth = new DateTimeOffset(model.Year, model.Month, 1, 0, 0, 0, TimeSpan.FromMinutes(model.Offset));
                var lastDateOfMonth = firstDateOfMonth.AddMonths(1);
                var tasks = await _taskService.GetTasksWithInRangeForUserAsync(userId,firstDateOfMonth, lastDateOfMonth);

                return Ok(tasks);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }
        }

        [HttpGet]
        [Route("within-range")]
        public async Task<IActionResult> GetTaskWithInRange([FromQuery] GetTaskWithInRangeModel model)
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound(new { UserId = "UserId not found" });

            if (DateTimeOffset.Compare(model.StartTime, model.EndTime) >= 0)
                return BadRequest(new { StartTime = "startTime must be earlier than endTime" });

            var tasks = await _taskService.GetTasksWithInRangeForUserAsync(userId, model.StartTime, model.EndTime);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("within-a-day")]
        public async Task<IActionResult> GetTaskWithInADay([FromQuery] GetTasksInADayModel model)
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound(new { UserId = "UserId not found" });
            try
            {
                var startTime = new DateTimeOffset(model.Year, model.Month, model.DayOfMonth, 0, 0, 0, TimeSpan.FromMinutes(model.Offset));
                var endTime = startTime.AddDays(1);
                var tasks = await _taskService.GetTasksWithInRangeForUserAsync(userId, startTime, endTime);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { DateFormat = ex.Message });
            }
        }
    }
}