using SharedObjects.Common;

namespace SharedObjects.IntermidiateModel
{
    public class TaskModel
    {
        public TaskModel()
        {
            CategoryId = default(Guid); // since categoryId in TaskEntity can be null, to make ValueExtractor class works properly, the CategoryId should
        }

        public Guid TaskId { get; set; }
        public ToDoTaskStatus? Status { get; set; }
        public string? Title { get; set; }
        public Guid? UserId { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public Guid? CategoryId { get; set; }
    }
}