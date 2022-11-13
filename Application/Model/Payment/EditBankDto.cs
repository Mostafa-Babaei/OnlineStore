using Application.Common.Validator;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class EditBankDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "عنوان درگاه")]
        public string Title { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "توضیحات درگاه")]
        public string Description { get; set; }

        public string Icon { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "وضعیت درگاه")]
        public bool IsActive { get; set; } = false;

        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [Display(Name = "اطلاعات درگاه")]
        public string AccountInfo { get; set; }

        [Display(Name = "تصویر درگاه")]
        [MaxFileSizeAttribute(200 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile PictureFile { get; set; }
    }
}
