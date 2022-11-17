using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel;

public class ChangePasswordModel
{
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}