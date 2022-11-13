using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Model;
using infrastructure.Models;
using Microsoft.AspNetCore.Authentication;

namespace infrastructure.Identity
{
    public interface IIdentityService
    {

        /// <summary>
        /// دریافت کاربر جاری
        /// </summary>
        /// <returns></returns>
        string GetCurrentUser(string userClaim);

        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApiResult> CreateUserAsync(RegisterDto model);

        /// <summary>
        /// دریافت اطلاعات کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ApiResult GetUser(string userId);

        Task<ApiResult> SignInUserAsync(string userName, string password);

        /// <summary>
        /// دریافت نام کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetUserNameAsync(string userId);

        /// <summary>
        /// دریافت نام کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetFullNameAsync(string userId);

        /// <summary>
        /// بررسی نقش کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> IsInRoleAsync(string userId, string role);

        /// <summary>
        /// دریافت اطلاعات کاربر بر اساس ایمیل
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ApiResult GetUserByEmail(string email);

        /// <summary>
        /// ایجاد لینک ریست پسورد
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetResetToken(ApplicationUser user);

        /// <summary>
        /// دریافت لیست کاربران
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetUsers();

        /// <summary>
        /// خروج کاربر
        /// </summary>
        Task SignOut();

        /// <summary>
        /// دریافت لیست نقش ها
        /// </summary>
        /// <returns></returns>
        List<IdentityRole> GetRoles();


        /// <summary>
        /// تغییر وضعیت کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ApiResult ChangeStateUser(string userId);

        /// <summary>
        /// تغییر رمز عبور کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult ChangePassword(ChangePasswordByAdminDto model);

        /// <summary>
        /// تغییر رمز عبور با توکن
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult ChangePasswordWithToken(ChangePasswordDto model);

        /// <summary>
        /// اعمال نقش به کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult SetRoleUser(SetUserRoleDto model);

        /// <summary>
        /// اصلاح نقش های کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult ModifyRoleUser(SetUserRoleDto model);

        /// <summary>
        /// دریافت خطا های احراز هویت
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        string GetErrorMessage(IEnumerable<IdentityError> errors);

        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ApiResult EditUser(ApplicationUser user);
        
        /// <summary>
        /// تعداد کاربران
        /// </summary>
        /// <returns></returns>
        int UserCount ();

        List<AuthenticationScheme> GetExternalLogins();

        AuthenticationProperties ConfigExternalLogin(string provider,string returnUrl);
        ApiResult ExternalLogin();

    }
}
