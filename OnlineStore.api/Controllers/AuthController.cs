using Application.Common.Model;
using Application.Constant.Message;
using infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IIdentityService identityService;
  
        public AuthController(IConfiguration configuration, IIdentityService identityService)
        {
            this.configuration = configuration;
            this.identityService = identityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ApiResult TestApi()
        {
            return ApiResult.ToSuccessModel("Test Ok");
        }


        [HttpGet]
        public ApiResult GetAllUser()
        {
            return ApiResult.ToSuccessModel("لیست کاربران", identityService.GetUsers().Result);
        }

        /// <summary>
        /// ورود کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult Login([FromBody] LoginApi user)
        {
            try
            {

                if (user is null)
                    return ApiResult.ToErrorModel("مقادیر ورودی اشتباه است");

                ApiResult signinState = identityService.SignInUserAsync(user.UserName, user.Password).Result;

                if (signinState == null)
                    return ApiResult.ToErrorModel("مقادیر ورودی اشتباه است");

                if (signinState == null || !signinState.IsSuccess)
                    return ApiResult.ToErrorModel(signinState.Message);

                if (signinState.IsSuccess)
                {
                    if (configuration["Jwt:Key"] == null)
                        return ApiResult.ToErrorModel("کلید احراز هویت نامعتبر است");

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: new List<Claim>() {
                        new Claim("userId", signinState.Data.ToString())
                        },
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    var claims = new List<Claim>
                    {
                        new Claim("userId",signinState.Data.ToString())
                    };

                    var appIdentity = new ClaimsIdentity(claims);
                    User.AddIdentity(appIdentity);
                    return ApiResult.ToSuccessModel("ورود کاربر با موفقیت انجام شد", tokenString);
                }
                return ApiResult.ToErrorModel(signinState.Message);
            }
            catch (Exception ex)
            {
                return ApiResult.ToSuccessModel(CommonMessage.UnhandledError);
            }
        }

        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult Register([FromBody] RegisterUserDto registerModel)
        {
            try
            {
                RegisterDto registerDto = new RegisterDto()
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    IsActive = true
                };
                var registerState = identityService.CreateUserAsync(registerDto).Result;
                if (!registerState.IsSuccess)
                    return registerState;

                var setRole= identityService.SetRoleUser(new SetUserRoleDto()
                {
                    Roles = new List<string>() { "Customer" },
                    UserId = registerState.Data.ToString()
                });

                return registerState;
            }
            catch (Exception ex)
            {
                return ApiResult.ToSuccessModel(CommonMessage.UnhandledError);
            }
        }

        /// <summary>
        /// بازنشانی رمز عبور
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiResult ResetPassword(string email)
        {
            //Todo:بررسی کاربر و ایجاد توکن و ارسال از طریق ایمیل 
            return ApiResult.ToSuccessModel("ایمیل بازنشانی برای شما ارسال شد");
        }

        /// <summary>
        /// تغییر رمز عبور توسط کاربر
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult ChangePassword([FromBody] ChangePasswordByUserDto changePasswordModel)
        {
            try
            {
                return identityService.ChangePassword(new ChangePasswordByAdminDto()
                {
                    UserId = changePasswordModel.UserId,
                    Password = changePasswordModel.Password
                });

            }
            catch (Exception ex)
            {
                return ApiResult.ToSuccessModel(CommonMessage.UnhandledError);
            }
        }

        [HttpPut]
        public ApiResult ChangeState(string userId)
        {
            try
            {
                return identityService.ChangeStateUser(userId);
            }
            catch (Exception ex)
            {
                return ApiResult.ToSuccessModel(CommonMessage.UnhandledError);
            }
        }


        /// <summary>
        /// دریافت نقش ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAllRole()
        {
            return ApiResult.ToSuccessModel("لیست نقش ها", identityService.GetRoles());
        }

        /// <summary>
        /// ثبت نقش جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddRole(string role)
        {
            return identityService.AddRole(role).Result;
        }

        /// <summary>
        /// دریافت نقش کاربر
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUserRole()
        {
            string userId = GetUser();
            if (userId == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");
            return identityService.GetRolesOfUser(userId);
        }

        /// <summary>
        /// تغییر رمز عبور کاربر
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult ChangeUserPassword(string newPassword)
        {
            string userId = GetUser();
            if (userId == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            return identityService.ChangePassword(new ChangePasswordByAdminDto()
            {
                Password = newPassword,
                UserId = userId
            });
        }

        /// <summary>
        /// ثبت نقش برای کاربر
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SetUserRole(string role)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");
            return identityService.SetRoleUser(new SetUserRoleDto()
            {
                UserId = userId,
                Roles = new List<string> { role }
            });
        }

        private string GetUser()
        {

            return this.User.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;
        }
    }
}
