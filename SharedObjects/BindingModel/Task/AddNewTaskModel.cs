using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class AddNewTaskModel
    {
        public AddNewTaskModel()
        {
            CategoryId = default(Guid);
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public Guid? CategoryId { get; set; }

        [MaxLength(150)]
        public string? Description { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}