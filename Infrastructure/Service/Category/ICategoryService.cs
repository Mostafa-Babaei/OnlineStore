using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Model;
using Application.Model;
using Domain.Entities;

namespace infrastructure.Service
{
    public interface ICategoryService
    {
        /// <summary>
        /// لیست دسته بندی ها
        /// </summary>
        /// <returns></returns>
        List<CategoryDto> GetAllCategory();

        /// <summary>
        /// دریافت دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Category GetCategory(int id);

        /// <summary>
        /// ثبت دسته بندی جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult InsertCategory(InsertCategoryDto model);

        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult EditCategory(EditCategoryDto model);

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult RemoveCategory(int id);

        /// <summary>
        /// بررسی وجود داشتن نام دسته بندی
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool ExistCategory(string category);
    }
}
