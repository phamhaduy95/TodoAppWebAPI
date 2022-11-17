using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class AddNewUserModel
    {
        [Required]
        public string? DisplayName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
    }
}