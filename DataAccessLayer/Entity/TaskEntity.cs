using SharedObjects.Common;

namespace DataAccessLayer.Entity
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public ToDoTaskStatus Status { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public CategoryEntity Category { get; set; }
        public Guid? CategoryId { get; set; }
    }
}