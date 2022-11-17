using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class AddCategoryModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Color { get; set; }
    }
}