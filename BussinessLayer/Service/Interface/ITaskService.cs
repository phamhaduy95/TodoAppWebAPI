using SharedObjects.Common;
using SharedObjects.IntermidiateModel;

namespace BussinessLayer.Service.Interface
{
    public interface ITaskService
    {
        public Task<ResponseResult> CreateTaskAsync(TaskModel model);

        public Task<TaskModel> GetTaskByIdAsync(Guid taskId);

        public Task<ICollection<TaskModel>> GetAllAsync();

        public Task<ICollection<TaskModel>> GetAllTasksForUserAsync(Guid userId);

        public Task<ICollection<TaskModel>> GetTasksWithInRangAsync(DateTimeOffset startDays, DateTimeOffset endDays);

        public Task<ICollection<TaskModel>> GetTasksByCategoryAsync(Guid CategoryId);

        public Task<ResponseResult> UpdateTaskAsync(TaskModel model);

        public Task<ICollection<TaskModel>> GetTasksWithInRangeForUserAsync(Guid userId, DateTimeOffset startDays, DateTimeOffset endDays);

        public Task<ResponseResult> DeleteTaskAsync(Guid taskId);
    }
}