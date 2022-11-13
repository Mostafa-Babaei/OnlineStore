using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class EditProductDto
    {

        public int ProductId { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string Title { get; set; }

        public string Description { get; set; }
        public string PicPath { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int Quntity { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public long Price { get; set; }

        [Display(Name = "دسته بندی محصول")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int ProductTypeId { get; set; }

        public List<int> ValuesSelected { get; set; }

    }
}
