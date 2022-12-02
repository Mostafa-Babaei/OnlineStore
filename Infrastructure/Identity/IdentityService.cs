using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Model;
using infrastructure.Models;
using Microsoft.AspNetCore.Authentication;

namespace infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signManager;
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signManager,
        RoleManager<IdentityRole> roleManager)
        //IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            this.signManager = signManager;
            this.roleManager = roleManager;
            //_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            if (userId == null) return "";
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            if (user == null) return "";
            return user.UserName;
        }
        public async Task<string> GetFullNameAsync(string userId)
        {
            if (userId == null) return "";
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            if (user == null) return "";
            return user.Fullname;
        }

        public async Task<ApiResult> CreateUserAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                Fullname = model.Fullname,
                UserName = model.Email,
                Email = model.Email,
                IsActive = model.IsActive,
                Avatar = "UserAvatar.png"
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in result.Errors)
                    errors.Add(error.Description);

                return ApiResult.ToErrorModel("خطا در ثبت کاربر", Data: errors);
            }

            return new ApiResult() { IsSuccess = result.Succeeded, Message = "ثبت نام کاربر با موفقیت انجام شد", Data = user.Id };
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<ApiResult> SignInUserAsync(string userName, string password)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userName);
            if (user == null)
                return ApiResult.ToErrorModel("کاربری با مشخصات فوق یافت نشد", null);

            var result = await signManager.PasswordSignInAsync(user, password, true, false);
            if (result.Succeeded)
            {
                //var claim = new Claim(newIdentity.RoleClaimType, role.Name);
                //userIdentity.AddClaim(new Claim("Fullname", user.Fullname));
                if (!user.IsActive)
                    return ApiResult.ToErrorModel("حساب کاربری شما غیر فعال است، لطفا با پشتیبان سایت هماهنگ بفرمائید", null);
                return ApiResult.ToSuccessModel(" خوش آمدید", user.Id);
            }
            if (result.IsNotAllowed)
                return ApiResult.ToErrorModel("نام کاربری و یا کلمه عبور اشتباه است", null);
            if (result.IsLockedOut)
                return ApiResult.ToErrorModel("حساب کاربری شما غیر فعال شده است", null);
            return ApiResult.ToErrorModel("خطا در ورود کاربر", null);
        }

        public ApiResult GetUserByEmail(string email)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                return ApiResult.ToErrorModel("کاربر مورد نظر یافت نشد", null);


            return ApiResult.ToSuccessModel("دریافت اطلاعات با موفقیت انجام شد", user);
        }

        public async Task<string> GetResetToken(ApplicationUser user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return code;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }

        public async Task SignOut()
        {
            await signManager.SignOutAsync();
        }

        public List<IdentityRole> GetRoles()
        {
            var roles = roleManager.Roles.ToListAsync().Result;
            return roles;
        }

        public ApiResult ChangeStateUser(string userId)
        {
            var userResult = GetUser(userId);
            if (!userResult.IsSuccess)
                return userResult;

            ApplicationUser user = (ApplicationUser)userResult.Data;
            user.IsActive = !user.IsActive;
            var result = _userManager.UpdateAsync(user).Result;
            if (!result.Succeeded)
                return ApiResult.ToErrorModel("خطا در تغییر وضعیت کاربر");

            return ApiResult.ToSuccessModel("وضعیت کاربر تغییر کرد", user.IsActive);
        }

        public ApiResult ChangePassword(ChangePasswordByAdminDto model)
        {

            var userResult = GetUser(model.UserId);
            if (!userResult.IsSuccess)
                return userResult;

            ApplicationUser user = (ApplicationUser)userResult.Data;

            var result = _userManager.RemovePasswordAsync(user).Result;
            if (!result.Succeeded)
                return ApiResult.ToErrorModel("خطا در حذف رمز عبور قبلی کاربر");

            result = _userManager.AddPasswordAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                string errorMessages = GetErrorMessage(result.Errors);
                return ApiResult.ToErrorModel(errorMessages);
            }

            return ApiResult.ToSuccessModel("رمز عبور کاربر با موفقیت  تغییر کرد", user.IsActive);
        }

        public ApiResult GetUser(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
                return ApiResult.ToErrorModel("خطا در دریافت اطلاعات کاربر");

            return ApiResult.ToSuccessModel("اطلاعات کاربر دریافت شد", user);

        }



        public ApiResult SetRoleUser(SetUserRoleDto model)
        {
            try
            {
                if (roleManager.RoleExistsAsync(model.Roles.First()).Result == false)
                    return ApiResult.ToErrorModel("نقش مورد نظر وجود ندارد");

                var userResult = GetUser(model.UserId);
                if (!userResult.IsSuccess)
                    return userResult;
                ApplicationUser user = (ApplicationUser)userResult.Data;

                var result = _userManager.AddToRolesAsync(user, model.Roles).Result;
                if (!result.Succeeded)
                    return ApiResult.ToErrorModel("خطا ثبت نقش کاربر");

                return ApiResult.ToSuccessModel("نقش کاربر دریافت شد");
            }
            catch (System.Exception ex)
            {

                return ApiResult.ToErrorModel("خطا ثبت نقش کاربر", exception: ex.ToString());
            }
        }



        public string GetErrorMessage(IEnumerable<IdentityError> errors)
        {
            StringBuilder errorMessages = new StringBuilder();
            foreach (var error in errors)
                errorMessages.AppendLine(error.Description + "<br />");

            return errorMessages.ToString();
        }

        public ApiResult EditUser(ApplicationUser user)
        {
            ApplicationUser userModify = _userManager.FindByIdAsync(user.Id).Result;
            userModify.Fullname = user.Fullname;
            userModify.Mobile = user.Mobile;
            userModify.NationalCode = user.NationalCode;
            userModify.IsActive = user.IsActive;
            userModify.Avatar = user.Avatar;
            var result = _userManager.UpdateAsync(userModify).Result;

            if (!result.Succeeded)
            {
                string errorMessages = GetErrorMessage(result.Errors);
                return ApiResult.ToErrorModel(errorMessages);
            }

            return ApiResult.ToSuccessModel("اطلاعات کاربر با موفقیت ویرایش شد");
        }

        public string GetCurrentUser(string email)
        {
            var user = GetUserByEmail(email);
            if (user.IsSuccess)
                return ((ApplicationUser)user.Data).Id;
            return "";
        }

        public ApiResult ModifyRoleUser(SetUserRoleDto model)
        {
            var userResult = GetUser(model.UserId);
            if (!userResult.IsSuccess)
                return userResult;
            ApplicationUser user = (ApplicationUser)userResult.Data;

            List<string> roles = roleManager.Roles.Select(e => e.Name).ToList();
            foreach (var item in roles)
            {
                if (model.Roles.Contains(item))
                {
                    if (!_userManager.IsInRoleAsync(user, item).Result)
                    {
                        var result = _userManager.AddToRoleAsync(user, item).Result;
                        if (!result.Succeeded)
                            return ApiResult.ToErrorModel("خطا ثبت نقش کاربر");
                    }
                }
                else
                {
                    if (_userManager.IsInRoleAsync(user, item).Result)
                    {
                        var result = _userManager.RemoveFromRoleAsync(user, item).Result;
                        if (!result.Succeeded)
                            return ApiResult.ToErrorModel("خطا حذف نقش کاربر");
                    }
                }
            }

            return ApiResult.ToSuccessModel("نقش کاربر اصلاح شد");

        }

        public int UserCount()
        {
            return _userManager.Users.Count();
        }

        public ApiResult ChangePasswordWithToken(ChangePasswordDto model)
        {
            ApiResult getUser = GetUser(model.UserId);
            if (!getUser.IsSuccess)
                return getUser;
            ApplicationUser user = (ApplicationUser)getUser.Data;
            var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
            if (!result.Succeeded)
            {
                string errors = GetErrorMessage(result.Errors);
                return ApiResult.ToErrorModel(errors);
            }
            return ApiResult.ToSuccessModel("تغییر رمز عبور با موفقیت انجام شد");
        }

        public List<AuthenticationScheme> GetExternalLogins()
        {
            return signManager.GetExternalAuthenticationSchemesAsync().Result.ToList();
        }

        public AuthenticationProperties ConfigExternalLogin(string provider, string returnUrl)
        {
            return signManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
        }

        public ApiResult ExternalLogin()
        {
            var checkLogin = signManager.GetExternalLoginInfoAsync().Result;
            if (checkLogin == null)
                return ApiResult.ToErrorModel("خطا در ورود کاربر");
            var signinResult = signManager.ExternalLoginSignInAsync(checkLogin.LoginProvider, checkLogin.ProviderKey, false, true).Result;
            if (signinResult.Succeeded)
                return ApiResult.ToSuccessModel("کاربر با موفقیت وارد شد");

            var email = checkLogin.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user == null)
                {
                    var userName = email.Split('@')[0];
                    user = new ApplicationUser()
                    {
                        UserName = (userName.Length <= 10 ? userName : userName.Substring(0, 10)),
                        Email = email,
                        EmailConfirmed = true
                    };

                    _userManager.CreateAsync(user);
                }

                _userManager.AddLoginAsync(user, checkLogin);
                signManager.SignInAsync(user, false);

                return ApiResult.ToSuccessModel("کاربر با موفقیت ثبت شد");
            }


            return ApiResult.ToErrorModel("خطا در ورود کاربر");
        }

        public async Task<ApiResult> AddRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return ApiResult.ToErrorModel("عنوان نقش درخواستی را وارد نمائید");

            if (await roleManager.RoleExistsAsync(role))
                return ApiResult.ToErrorModel("نقش درخواستی تکراری می باشد");

            IdentityResult result = await roleManager.CreateAsync(new IdentityRole()
            {
                Name = role
            });

            if (!result.Succeeded)
                return ApiResult.ToErrorModel("خطا در ثبت نقش");

            return ApiResult.ToSuccessModel("نقش با وفقیت ثبت شد");
        }

        public ApiResult GetRolesOfUser(string userId)
        {
            var user = GetUser(userId);

            if (user == null)
                return ApiResult.ToErrorModel("");

            if (!user.IsSuccess)
                return ApiResult.ToErrorModel(user.Message);
            var roles = _userManager.GetRolesAsync((ApplicationUser)user.Data).Result;
            return ApiResult.ToSuccessModel(user.Message, roles);
        }

        public ApiResult RemoveRoleFromUser(string userId, string roleName)
        {
            var userResult = GetUser(userId);
            if (!userResult.IsSuccess)
                return userResult;
            ApplicationUser user = (ApplicationUser)userResult.Data;

            var result = _userManager.RemoveFromRoleAsync(user, roleName).Result;
            if (!result.Succeeded)
                return ApiResult.ToErrorModel("خطا حذف نقش کاربر");

            return ApiResult.ToSuccessModel("نقش کاربر حذف شد");
        }
    }
}
