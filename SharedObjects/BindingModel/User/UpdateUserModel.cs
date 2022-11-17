using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class UpdateUserModel
    {
        [MaxLength(50)]
        public string? DisplayName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Organization { get; set; }
    }
}