using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace infrastructure.Identity
{
    public class AccountDto
    {
    }

    public class LoginApi
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام کاربری")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RemmemberMe { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "وارد نمودن آدرس ایمیل الزامیست")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل صحیح را وارد نمائید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class ChangePasswordDto
    {

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "کاربر یافت نشد")]
        public string UserId { get; set; }

        [Required(ErrorMessage = " {0} نامعتبر است")]
        [Display(Name = "لینک بازنشانی")]
        public string Token { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }


        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور ها یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordByAdminDto
    {

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "کاربر یافت نشد")]
        public string UserId { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }

    }

    public class RegisterByUserDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام کاربری")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Compare(nameof(Password), ErrorMessage = "کلمه های عبور وارد شده یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [EmailAddress(ErrorMessage = "ایمیل صحیح را وارد نمائید")]
        [Display(Name = "نام کاربری(ایمیل)")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Fullname { get; set; }


        [Display(Name = "نقش")]
        public List<SelectListItem> Roles { get; set; }

        [Display(Name = "وضعیت کاربر")]
        public bool IsActive { get; set; }
    }

    public class EditUserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Fullname { get; set; }

        [Display(Name = "شماره موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string Avatar { get; set; }

        [Display(Name = "نقش های کاربر")]
        public List<SelectListItem> Roles { get; set; }

        [Display(Name = "وضعیت کاربر")]
        public bool IsActive { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile PictureFile { get; set; }
    }

    public class SetUserRoleDto
    {

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "کاربر یافت نشد")]
        public string UserId { get; set; }


        [Display(Name = "لیست نقش ها")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public List<string> Roles { get; set; }

    }

    public class RegisterUserDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [EmailAddress(ErrorMessage = "ایمیل صحیح را وارد نمائید")]
        [Display(Name = "نام کاربری(ایمیل)")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }
    }

    public class ChangePasswordByUserDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string UserId { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور ها یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }




}
