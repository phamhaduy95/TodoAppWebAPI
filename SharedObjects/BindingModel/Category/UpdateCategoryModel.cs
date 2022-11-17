using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class UpdateCategoryModel
    {
        [Required]
        public Guid CategoryId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public string? Color { get; set; }
    }
}