using BussinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity;
using SharedObjects.Common;
using SharedObjects.IntermidiateModel;
using SharedObjects.Utils;

namespace BussinessLayer.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ITokenGenerateService _tokenService;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ITokenGenerateService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseResult> AddUserAsync(UserModel model)
        {
            var newUser = new UserEntity() { };
            ValueExtractor.ExtractValueFromModelToEntity(newUser, model);
            newUser.Id = Guid.NewGuid();
            newUser.NormalizedEmail = model.Email;
            newUser.UserName = newUser.Id.ToString();
            newUser.NormalizedUserName = newUser.Id.ToString();
            newUser.JoinDate = DateTime.Now;
            newUser.SecurityStamp = DateTimeOffset.Now.ToString();
            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded) return ResponseResult.GetSuccessResult();
            return ResponseResult.DataBaseError();
        }

        public async Task<ResponseResult> ChangeEmailAsync(Guid userId, string newEmail, string password)
        {
            var userWithEmail = await _userManager.FindByEmailAsync(newEmail);
            if (userWithEmail != null) return ResponseResult.GetFailResult(new { NewEmail = "email has already existed" });
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) return ResponseResult.GetFailResult(new { Password = "wrong password" });
            var result1 = await _userManager.SetEmailAsync(user, newEmail);
            if (!result1.Succeeded) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ResponseResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ResponseResult.NotFound(new { UserId = "not found" });
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded) return ResponseResult.GetFailResult(result.Errors);
            result = await _userManager.UpdateSecurityStampAsync(user);
            if (!result.Succeeded) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ResponseResult> DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ResponseResult.NotFound(new { UserId = "not found" });
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return ResponseResult.GetSuccessResult();
            return ResponseResult.DataBaseError();
        }

        public ICollection<UserModel> GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new UserModel
            {
                Id = u.Id,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                JoinDate = u.JoinDate,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Organization = u.Organization,
                DisplayName = u.DisplayName,
            }).ToList();
            return users;
        }

        public async Task<UserModel?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                JoinDate = user.JoinDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Organization = user.Organization,
                DisplayName = user.DisplayName,
            };
        }

        public async Task<ResponseResult> SignInAsync(string userEmail, string password)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return ResponseResult.NotFound(new { Email = "email not found" });
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) return ResponseResult.GetFailResult(new { Password = "wrong Password" });
            var strToken = _tokenService.GenerateToken(user);
            return ResponseResult.GetSuccessResult($"bearer {strToken}");
        }

        public async Task<ResponseResult> SignOutAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ResponseResult.NotFound(new { Email = "email not found" });
            var result = await _userManager.UpdateSecurityStampAsync(user);
            if (result.Succeeded) return ResponseResult.GetSuccessResult();
            return ResponseResult.DataBaseError();
        }

        public async Task<ResponseResult> SignUpAsync(UserModel model)
        {
            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithEmail != null) return ResponseResult.GetFailResult(new { Email = "email has already been used" });
            var result = await AddUserAsync(model);
            if (!result.Succeed) return result;
            var newAddedUser = await _userManager.FindByEmailAsync(model.Email);
            var token = _tokenService.GenerateToken(newAddedUser);
            return ResponseResult.GetSuccessResult($"bearer {token}");
        }

        public async Task<ResponseResult> UpdateUserAsync(UserModel model)
        {
            var userId = model.Id;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ResponseResult.NotFound(new { UserId = "account not found" });
            if (model.Email != null)
            {
                var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userWithEmail != null) return ResponseResult.GetFailResult(new { Email = "email has already been used" });
            }
            ValueExtractor.ExtractValueFromModelToEntity(user, model);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return ResponseResult.GetSuccessResult();
            return ResponseResult.DataBaseError();
        }
    }
}