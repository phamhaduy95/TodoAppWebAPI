using SharedObjects.IntermidiateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ResponseModel
{
    public class ReturnUserModel
    {
        public ReturnUserModel(UserModel model)
        {
            DisplayName = model.DisplayName?? "";
            Email = model.Email;
            PhoneNumber = model.PhoneNumber?? "";
            Organization = model.Organization ?? "";
            FirstName = model.FirstName?? "";
            LastName = model.LastName ?? "";
            Address = model.Address ?? "";
        }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Organization { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
    }
}
