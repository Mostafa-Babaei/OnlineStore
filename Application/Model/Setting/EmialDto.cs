using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class EmailDto
    {

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "SSL")]
        public bool SSLState { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "پورت")]
        public string SMTPPort { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "آدرس سرور")]
        public string Host{ get; set; }
    }
}
