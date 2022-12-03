using Application.Common;
using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using AutoMapper;
using infrastructure.Identity;
using infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("originList")]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<SiteSetting> option;
        private readonly IConfiguration configuration;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public AuthController(IOptions<SiteSetting> option, IConfiguration configuration, IIdentityService identityService, IMapper mapper)
        {
            this.option = option;
            this.configuration = configuration;
            this.identityService = identityService;
            this.mapper = mapper;
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
        /// ثبت کاربر جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddNewUser([FromBody] RegisterUserDto registerModel)
        {
            RegisterDto model = new RegisterDto()
            {
                Email = registerModel.Email,
                IsActive = true,
                Password = registerModel.Password,
                Fullname = registerModel.Fullname
            };
            var result = identityService.CreateUserAsync(model).Result;
            //تعریف نقش
            if (result.IsSuccess)
            {
                var setRole = identityService.SetRoleUser(new SetUserRoleDto()
                {
                    Roles = new List<string>() { "Customer" },
                    UserId = result.Data.ToString()
                });
            }
            return result;
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

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("userId", signinState.Data.ToString()));
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    //string userId = GetUser();
                    //var roles = identityService.GetRolesOfUser(signinState.Data.ToString()).Data;
                    //List<string> roles = new List<string>();
                    //LoginResultDto model = new LoginResultDto()
                    //{
                    //    Token = tokenString,
                    //    Roles = (List<string>)roles
                    //};

                    var appIdentity = new ClaimsIdentity(claims);
                    User.AddIdentity(appIdentity);
                    return ApiResult.ToSuccessModel("ورود کاربر با موفقیت انجام شد", tokenString);
                }
                return ApiResult.ToErrorModel(signinState.Message);
            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel(CommonMessage.UnhandledError, exception: ex.ToString());
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

                var setRole = identityService.SetRoleUser(new SetUserRoleDto()
                {
                    Roles = new List<string>() { "Customer" },
                    UserId = registerState.Data.ToString()
                });

                return registerState;
            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel(CommonMessage.UnhandledError, exception: ex.ToString());
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
                return ApiResult.ToErrorModel(CommonMessage.UnhandledError, exception: ex.ToString());
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
                return ApiResult.ToErrorModel(CommonMessage.UnhandledError, exception: ex.ToString());
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
            try
            {
                string userId = GetUser();
                if (userId == null)
                    return ApiResult.ToErrorModel("کاربر یافت نشد");
                return identityService.GetRolesOfUser(userId);
            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel("خطا در دریافت نقش کاربر", exception: ex.ToString());
            }

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

        /// <summary>
        /// ثبت نقش برای کاربر
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public bool IsInRole(string role)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return false;
            var t = identityService.IsInRoleAsync(userId, role).Result;
            return t;
        }

        /// <summary>
        /// دریافت اطلاعات کاربر جاری
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetCurrentUser()
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            var user = identityService.GetUser(userId);
            var userDto = mapper.Map<UserDto>(user.Data);
            userDto.avatar = option.Value.AvatarUrl + userDto.avatar;

            userDto.role = identityService.GetRoles().Select(e => new SelectListItem()
            {
                Text = e.Name,
                Value = e.Id,
                Selected = identityService.IsInRoleAsync(userId, e.Name).Result
            }).ToList();
            return ApiResult.ToSuccessModel(user.Message, userDto);
        }

        /// <summary>
        /// دریافت کاربر با شناسه
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetCurrentUserById(string userId)
        {
            var user = identityService.GetUser(userId);
            var userDto = mapper.Map<UserDto>(user.Data);
            userDto.avatar = option.Value.AvatarUrl + userDto.avatar;

            userDto.role = identityService.GetRoles().Select(e => new SelectListItem()
            {
                Text = e.Name,
                Value = e.Id,
                Selected = identityService.IsInRoleAsync(userId, e.Name).Result
            }).ToList();

            return ApiResult.ToSuccessModel(user.Message, userDto);
        }

        [HttpPut]
        public ApiResult UpdateUserById(UserDto user)
        {

            var result = identityService.GetUser(user.userId);
            if (!result.IsSuccess)
                return result;
            ApplicationUser applicationUser = (ApplicationUser)result.Data;
            applicationUser.Email = user.email;
            applicationUser.Fullname = user.fullname;
            applicationUser.Mobile = user.mobile;
            applicationUser.NationalCode = user.nationalCode;
            applicationUser.IsActive = user.isActive;
            var editResult = identityService.EditUser(applicationUser);
            if (editResult.IsSuccess)
            {
                //اصلاح نقش کاربر
                foreach (var item in user.role)
                {
                    bool isInRole = identityService.IsInRoleAsync(user.userId, item.Text).Result;
                    if (item.Selected && isInRole == false)
                    {
                        SetUserRoleDto userRole = new SetUserRoleDto();
                        userRole.Roles = new List<string>() { item.Text };
                        userRole.UserId = user.userId;

                        identityService.SetRoleUser(userRole);
                    }
                    if (!item.Selected && isInRole)
                    {
                        identityService.RemoveRoleFromUser(user.userId, item.Text);
                    }
                }
            }
            return editResult;
        }


        [HttpPut]
        public ApiResult UpdateUser(UserDto user)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");
            var result = identityService.GetUser(userId);
            if (!result.IsSuccess)
                return result;
            ApplicationUser applicationUser = (ApplicationUser)result.Data;
            applicationUser.Email = user.email;
            applicationUser.Fullname = user.fullname;
            applicationUser.Mobile = user.mobile;
            applicationUser.NationalCode = user.nationalCode;
            return identityService.EditUser(applicationUser);

        }


        [HttpPost]
        public ApiResult AddAvatar(IFormFile avatar)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            if (avatar == null)
                return ApiResult.ToErrorModel("انتخاب تصویر الزامیست");
            string formatFile = System.IO.Path.GetExtension(avatar.FileName);
            string newName = Guid.NewGuid().ToString() + formatFile;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/UserAvatar/", newName);


            var result = identityService.GetUser(userId);
            if (!result.IsSuccess)
                return result;
            ApplicationUser applicationUser = (ApplicationUser)result.Data;
            string oldImage = applicationUser.Avatar;

            applicationUser.Avatar = newName;
            var addresult = identityService.EditUser(applicationUser);

            if (addresult.IsSuccess)
            {

                //حذف عکس قدیمی
                if (!string.IsNullOrEmpty(oldImage))
                {
                    string pathimg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/UserAvatar/", oldImage);
                    if (System.IO.File.Exists(pathimg))
                        System.IO.File.Delete(pathimg);
                }

                //ثبت عکس جدید
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    avatar.CopyTo(stream);
                }
            }

            return addresult;
        }

        [HttpGet]
        public ApiResult Logout()
        {
            try
            {
                string userId = GetUser();
                if (GetUser() == null)
                    return ApiResult.ToErrorModel("کاربر یافت نشد");

                identityService.SignOut();
                return ApiResult.ToSuccessModel("خروج با موفقیت انجام شد");
            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel("خطا در خروج کاربر", exception: ex.ToString());
            }
        }

        private string GetUser()
        {
            return this.User.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;
        }
    }
}
