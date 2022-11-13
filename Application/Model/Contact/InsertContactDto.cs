using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Application.Model
{
    public class InsertContactDto
    {

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام و نام خانوادگی")]
        public string Fullname { get; set; }


        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "موضوع")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "متن پیام")]
        public string Content { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "شماره موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل صحیح را وارد نمائید")]
        public string Email { get; set; }
    }
}
