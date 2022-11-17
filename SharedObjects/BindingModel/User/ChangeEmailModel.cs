using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.BindingModel.User
{
    public class ChangeEmailModel
    {
        [Required]
        public string NewEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
