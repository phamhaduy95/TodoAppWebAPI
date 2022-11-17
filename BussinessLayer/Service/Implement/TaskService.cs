using BussinessLayer.Service.Interface;
using DataAccessLayer.EF;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using SharedObjects.Common;
using SharedObjects.IntermidiateModel;
using SharedObjects.Utils;

namespace BussinessLayer.Service.Implement
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _appDbContext;

        public TaskService(AppDbContext appDbContext, ITokenGenerateService tokenService)
        {
            _appDbContext = appDbContext;
        }

        private async Task<TaskEntity?> FindTaskEntityAsync(Guid taskId)
        {
            var task = await (from t in _appDbContext.Tasks
                              where t.TaskId == taskId
                              select t).FirstOrDefaultAsync();
            return task;
        }

        public async Task<ResponseResult> CreateTaskAsync(TaskModel model)
        {

            var newTaskEntity = new TaskEntity();
            ValueExtractor.ExtractValueFromModelToEntity(newTaskEntity, model);
            _appDbContext.Add(newTaskEntity);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ResponseResult> DeleteTaskAsync(Guid taskId)
        {
            var task = await FindTaskEntityAsync(taskId);
            if (task == null) return ResponseResult.NotFound(new { TaskId = "taskId not found" });
            _appDbContext.Tasks.Remove(task);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ICollection<TaskModel>> GetAllAsync()
        {
            var tasks = await (from t in _appDbContext.Tasks
                               select new TaskModel
                               {
                                   TaskId = t.TaskId,
                                   StartTime = t.StartTime,
                                   Title = t.Title,
                                   EndTime = t.EndTime,
                                   CreatedTime = t.CreatedTime,
                                   Description = t.Description,
                                   UserId = t.UserId,
                                   Status = t.Status,
                                   CategoryId = t.CategoryId,
                               }).ToListAsync();
            return tasks;
        }

        public async Task<TaskModel?> GetTaskByIdAsync(Guid taskId)
        {
            var task = await (from t in _appDbContext.Tasks
                              where t.TaskId == taskId
                              select new TaskModel
                              {
                                  TaskId = t.TaskId,
                                  CreatedTime = t.CreatedTime,
                                  Description = t.Description,
                                  StartTime = t.StartTime,
                                  EndTime = t.EndTime,
                                  Title = t.Title,
                                  UserId = t.UserId,
                                  Status = t.Status,
                                  CategoryId = t.CategoryId,
                              }).FirstOrDefaultAsync();
            return task;
        }

        public async Task<ICollection<TaskModel>> GetTasksWithInRangAsync(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var tasks = await (from t in _appDbContext.Tasks
                               where !(t.StartTime >= endTime || t.EndTime <= startTime)
                               select new TaskModel
                               {
                                   TaskId = t.TaskId,
                                   CreatedTime = t.CreatedTime,
                                   Description = t.Description,
                                   StartTime = t.StartTime,
                                   EndTime = t.EndTime,
                                   Title = t.Title,
                                   UserId = t.UserId,
                                   Status = t.Status
                                  ,
                                   CategoryId = t.CategoryId,
                               }).ToListAsync();

            return tasks;
        }

        public async Task<ResponseResult> UpdateTaskAsync(TaskModel model)
        {
            var task = await GetTaskByIdAsync(model.TaskId);
            if (task == null) return ResponseResult.NotFound("taskId not found");
            var taskEntity = new TaskEntity();
            ValueExtractor.ExtractValueFromModelToEntity(taskEntity, task);
            ValueExtractor.ExtractValueFromModelToEntity(taskEntity, model);
            _appDbContext.Tasks.Update(taskEntity);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ICollection<TaskModel>> GetAllTasksForUserAsync(Guid userId)
        {
            var tasks = await (from t in _appDbContext.Tasks
                               where t.UserId == userId
                               select new TaskModel
                               {
                                   CategoryId = t.CategoryId,
                                   CreatedTime = t.CreatedTime,
                                   UserId = userId,
                                   Description = t.Description,
                                   EndTime = t.EndTime,
                                   StartTime = t.StartTime,
                                   Status = t.Status,
                                   TaskId = t.TaskId,
                                   Title = t.Title,
                               }
                           ).ToListAsync();
            return tasks;
        }

        public async Task<ICollection<TaskModel>> GetTasksByCategoryAsync(Guid CategoryId)
        {
            var tasksList = await (from t in _appDbContext.Tasks
                                   where CategoryId == t.CategoryId
                                   select new TaskModel
                                   {
                                       CategoryId = t.CategoryId,
                                       CreatedTime = t.CreatedTime,
                                       UserId = t.UserId,
                                       Description = t.Description,
                                       EndTime = t.EndTime,
                                       StartTime = t.StartTime,
                                       Status = t.Status,
                                       TaskId = t.TaskId,
                                       Title = t.Title,
                                   }
                                   ).ToListAsync();
            return tasksList;
        }

        public async Task<ICollection<TaskModel>> GetTasksWithInRangeForUserAsync(Guid userId, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var tasks = await (from t in _appDbContext.Tasks
                               where t.UserId == userId && !(t.StartTime >= endTime || t.EndTime <= startTime)
                               select new TaskModel
                               {
                                   TaskId = t.TaskId,
                                   CreatedTime = t.CreatedTime,
                                   Description = t.Description,
                                   StartTime = t.StartTime,
                                   EndTime = t.EndTime,
                                   Title = t.Title,
                                   UserId = t.UserId,
                                   Status = t.Status
                                  ,
                                   CategoryId = t.CategoryId,
                               }).ToListAsync();

            return tasks;
        }
    }
}