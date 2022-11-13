using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class SendEmailDto
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده صحیح نمی باشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "متن پیام")]
        public string Contentn { get; set; }
    }
}
