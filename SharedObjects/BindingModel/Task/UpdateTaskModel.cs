using SharedObjects.Common;
using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class UpdateTaskModel
    {
        public UpdateTaskModel()
        {
            CategoryId = default(Guid);
        }

        public ToDoTaskStatus? Status { get; set; }
        public Guid? CategoryId { get; set; }

        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(150)]
        public string? Description { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}