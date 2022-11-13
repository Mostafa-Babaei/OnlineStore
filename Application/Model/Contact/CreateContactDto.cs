using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class CreateContactDto
    {

        [Display(Name = "اسمتون چیه؟")]
        [Required(ErrorMessage = "اسمت رو بگو دیگه :)")]
        public string Fullname { get; set; }


        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "پیامت درباره چیه؟")]
        public string Subject { get; set; }


        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "چرا پیام نزاشتی؟")]
        public string Content { get; set; }


        [Display(Name = "شماره همراهتون ؟")]
        [Required(ErrorMessage = "موبایلت یادت رفت!")]
        [StringLength(11, ErrorMessage = "موبایلت 11 رقمی نیست؟", MinimumLength = 11)]
        public string Mobile { get; set; }


        [Display(Name = "آدرس ایمیل؟")]
        [Required(ErrorMessage = "لطفا ایمیلت رو وارد کن")]
        [EmailAddress(ErrorMessage = "")]
        public string Email { get; set; }

    }
}
