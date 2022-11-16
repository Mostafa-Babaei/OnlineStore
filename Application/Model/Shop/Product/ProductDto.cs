using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class ProductDto
    {

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }

        [Display(Name = "عنوان محصول")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        public string PicPath { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "تعداد")]
        public int Quntity { get; set; }

        [Display(Name = "مبلغ")]
        public int Price { get; set; }

        [Display(Name = "شناسه دسته بندی")]
        public int CategoryId { get; set; }

        [Display(Name = "نام دسته بندی")]
        public int CategoryName { get; set; }

        [Display(Name = "برند محصول")]
        public int BrandId { get; set; }

        [Display(Name = "نام دسته بندی")]
        public int BrandName { get; set; }
    }
}
