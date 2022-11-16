using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace infrastructure.Service
{
    public class CategoryService : ICategoryService
    {
        //Todo:برای متن پیام ها متغیر ایجاد شود
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IMapper mapper;
        //private readonly ILogger logger;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            //this.logger = logger;
        }
        public ApiResult EditCategory(EditCategoryDto model)
        {
            try
            {
                Category category = GetCategory(model.Id);
                if (category == null)
                    return ApiResult.ToErrorModel("دسته بندی یافت نشد");

                if (ExistCategory(model.CategoryName))
                    return ApiResult.ToErrorModel("نام دسته بندی تکراری می باشد");

                category.CategoryName = model.CategoryName;
                category.IsEnable = model.IsEnable;
                category.Order = model.Order;
                var result = categoryRepository.SaveEntity();

                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در ویرایش دسته بندی");

                return ApiResult.ToSuccessModel("ویرایش دسته بندی با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "دریافت ویرایش دسته بندی");
                return ApiResult.ToErrorModel("خطا در ویرایش دسته بندی");
            }
        }

        public bool ExistCategory(string category)
        {
            try
            {
                var result = categoryRepository.Find(e => e.CategoryName == category).SingleOrDefault();
                if (result == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "دریافت بررسی دسته بندی");
                return false;
            }
        }

        public List<Category> GetAllCategory()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = categoryRepository.GetAll().ToList();
                return categories;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "دریافت اطلاعات دسته بندی");
                return categories;
            }
        }

        public Category GetCategory(int id)
        {
            Category category = new Category();
            try
            {
                category = categoryRepository.GetById(id);
                return category;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "دریافت دسته بندی");
                return category;
            }
        }

        public ApiResult InsertCategory(InsertCategoryDto model)
        {
            try
            {
                Category category = mapper.Map<Category>(model);
                var result = categoryRepository.Add(category);
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
                return ApiResult.ToSuccessModel("ثبت دسته بندی با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            }
        }

        public ApiResult RemoveCategory(int id)
        {
            try
            {
                var category = GetCategory(id);
                if (category == null)
                    return ApiResult.ToErrorModel("دسته بندی یافت نشد");

                var result = categoryRepository.Remove(category);
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در حذف دسته بندی");
                return ApiResult.ToSuccessModel("حذف دسته بندی با موفقیت ثبت شد");

            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در حذف دسته بندی");
                return ApiResult.ToErrorModel("خطا در حذف دسته بندی");
            }
        }
    }
}
