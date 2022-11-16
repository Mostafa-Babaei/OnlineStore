using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    /// <summary>
    /// ویرایش دسته بندی
    /// </summary>
    public class EditCategoryDto
    {
        public int Id { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "کاربر یافت نشد")]
        public string CategoryName { get; set; }


        [Display(Name = "دسته بندی اصلی")]
        public int? ParentId { get; set; }

        [Display(Name = "جایگاه")]
        public int Order { get; set; } = 1;

        [Display(Name = "وضعیت")]
        public bool IsEnable { get; set; } = true;
    }
}
