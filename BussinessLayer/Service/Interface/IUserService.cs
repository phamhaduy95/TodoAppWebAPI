using SharedObjects.Common;
using SharedObjects.IntermidiateModel;

namespace BussinessLayer.Service.Interface
{
    public interface IUserService
    {
        public Task<ResponseResult> SignInAsync(string userEmail, string password);

        public Task<ResponseResult> SignOutAsync(Guid userId);

        public Task<ResponseResult> SignUpAsync(UserModel model);

        public Task<ResponseResult> AddUserAsync(UserModel model);

        public Task<ResponseResult> UpdateUserAsync(UserModel model);

        public Task<ResponseResult> DeleteUserAsync(Guid userId);

        public Task<UserModel> GetUserByIdAsync(Guid userId);

        public ICollection<UserModel> GetAllUsers();

        public Task<ResponseResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

        public Task<ResponseResult> ChangeEmailAsync(Guid userId, string newEmail, string password);
    }
}