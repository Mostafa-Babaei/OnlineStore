using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "وضعیت محصول")]
        public bool IsActive { get; set; }

        public string PicPath { get; set; }


        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int Quntity { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int Price { get; set; }

        [Display(Name = "دسته بندی محصول")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int CategoryId { get; set; }


        [Display(Name = "برند محصول")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int BrandId { get; set; }

        public List<SelectListItem> ListOfCategory { get; set; }


        [Display(Name = "تصویر محصول")]
        public IFormFile ProductImage { get; set; }

    }
}
