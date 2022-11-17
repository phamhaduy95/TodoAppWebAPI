using BussinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedObjects.BindingModel;
using SharedObjects.BindingModel.User;
using SharedObjects.IntermidiateModel;
using SharedObjects.ResponseModel;

namespace ToDoAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Policy = "SecurityTokenCheck")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDataProtector _dataProtector;
        private readonly CookieOptions _cookieOptions; // to able to correctly delete the cookie, cookieOptions must be similar in both append and delete operator.

        public UserController(IUserService userService, IDataProtectionProvider provider)
        {
            _userService = userService;
            _dataProtector = provider.CreateProtector("brearer_token_in_cookie.v1");
            _cookieOptions = new CookieOptions();
            _cookieOptions.HttpOnly = true;
            _cookieOptions.Expires = DateTimeOffset.UtcNow.AddDays(2);
            _cookieOptions.IsEssential = true;
            _cookieOptions.SameSite = SameSiteMode.Lax; // set none so that browser can save cookie in COR site.
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var token = HttpContext.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token)) return Ok();
            var response = await _userService.SignInAsync(model.Email, model.Password);
            if (response.Succeed)
            {
                if (model.Client == "browser")
                {
                    var encryptedToken = _dataProtector.Protect(response.Message);
                    HttpContext.Response.Cookies.Append("token", encryptedToken, _cookieOptions);
                    return Ok();
                }
                return Ok(new { token = response.Message });
            }
            return response.GenerateActionResult();
        }

        [HttpPost]
        [Route("/api/signout")]
        public async Task<IActionResult> SignOut()
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            var response = await _userService.SignOutAsync(userId);

            if (response.Succeed)
            {
                HttpContext.Response.Cookies.Delete("token", _cookieOptions);
                return Ok(new { Message = "successfully log out" });
            }
            return response.GenerateActionResult();
        }

        [HttpGet]
        [Route("/api/authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);

            var user = await _userService.GetUserByIdAsync(userId);
            var returnedData = new ReturnUserModel(user);
            return Ok(returnedData);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var passwordHasher = new PasswordHasher<UserEntity>();
            var newUser = new UserModel
            {
                PasswordHash = passwordHasher.HashPassword(null, model.Password),
                Email = model.Email,
                DisplayName = model.DisplayName,
                NormalizedEmail = model.Email,
            };
            var respone = await _userService.SignUpAsync(newUser);
            if (!respone.Succeed) return respone.GenerateActionResult();
            var token = respone.Message;
            if (model.Client == "browser")
            {
                var encryptedToken = _dataProtector.Protect(respone.Message);
                HttpContext.Response.Cookies.Append("token", encryptedToken, _cookieOptions);
                return Ok();
            }
            return Ok(new { token = token });
        }

        [HttpPut]
        [Route("/api/user-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            var response = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            return response.GenerateActionResult();
        }

        [HttpPut]
        [Route("/api/user-data")]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserModel model)
        {
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            var user = new UserModel
            {
                Id = userId,
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Organization = model.Organization,
            };
            var response = await _userService.UpdateUserAsync(user);
            return response.GenerateActionResult();
        }
        [HttpPut]
        [Route("/api/user-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailModel model)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);
            Guid userId;
            Guid.TryParse(User.FindFirst(c => c.Type == "userId")?.Value, out userId);
            var response = await _userService.ChangeEmailAsync(userId, model.NewEmail, model.Password);
            return response.GenerateActionResult();
        }
    }
}