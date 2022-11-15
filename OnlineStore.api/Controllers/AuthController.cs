using Application.Common.Model;
using Application.Constant.Message;
using infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IIdentityService identityService;

        public AuthController(IConfiguration configuration, IIdentityService identityService)
        {
            this.configuration = configuration;
            this.identityService = identityService;
        }

        /// <summary>
        /// ورود کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
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
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
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
        [HttpPost("register")]
        public ApiResult Registe([FromBody] RegisterUserDto registerModel)
        {
            try
            {
                RegisterDto registerDto = new RegisterDto()
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    IsActive = true
                };
                return identityService.CreateUserAsync(registerDto).Result;

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
        [HttpGet("reset-password")]
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
        [HttpPost("change-password")]
        [Authorize]
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


    }
}
