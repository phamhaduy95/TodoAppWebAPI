using BussinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedObjects.BindingModel;
using SharedObjects.Common;
using SharedObjects.IntermidiateModel;

namespace ToDoAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(Policy = "SecurityTokenCheck")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public CategoryController(ICategoryService categoryService, IUserService userService,
           ITaskService taskService)
        {
            _categoryService = categoryService;
            _userService = userService;
            _taskService = taskService;
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);
            if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            return Ok(category);
        }

        [HttpGet]
        [Route("{categoryId}/tasks")]
        public async Task<IActionResult> GetAllTasksFromCategory(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);
            if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            var tasks = await _taskService.GetTasksByCategoryAsync(categoryId);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);

            if (userId == default(Guid))
            {
                var list = await _categoryService.GetAllAsync();
                return Ok(list);
            }
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound(new ErrorMessage("userId not found"));
            var categories = await _categoryService.GetAllForUserAsync(userId);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryModel model)
        {

            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryToAdd = new CategoryModel
            {
                CategoryId = new Guid(),
                Name = model.Name,
                UserId = userId,
                Description = model.Description,
                Color = model.Color,
            };
            var response = await _categoryService.AddNewCategoryAsync(categoryToAdd);
            return response.GenerateActionResult();
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> RemoveCategory(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);
            if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            var response = await _categoryService.RemoveCategoryAsync(categoryId);
            return response.GenerateActionResult();
        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryModel model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var category = await _categoryService.GetCategoryAsync(categoryId);
            if (category == null) return NotFound(new ErrorMessage("categoryId not found"));
            var categoryToUpdate = new CategoryModel
            {
                CategoryId = categoryId,
                Name = model.Name,
                Color = model.Color,
                Description = model.Description,
            };
            var response = await _categoryService.UpdateCategoryAsync(categoryToUpdate);
            return response.GenerateActionResult();
        }
    }
}