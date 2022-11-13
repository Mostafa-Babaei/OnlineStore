using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Common.Model;
using Domain.Entities;
using infrastructure.Repository;

namespace infrastructure.Service
{
    class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<MainMenu> categoryRepository;

        public CategoryService(IGenericRepository<MainMenu> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public ApiResult EditCategory(MainMenu tag)
        {
            throw new NotImplementedException();
        }

        public bool ExistCategory(string category)
        {
            var result = categoryRepository.Find(e => e.CategoryName == category).SingleOrDefault();
            if (result == null) return false;
            return true;
        }

        public List<MainMenu> GetAllCategory()
        {
            return categoryRepository.GetAll().ToList();
        }

        public MainMenu GetCategory(int id)
        {
            return categoryRepository.GetById(id);
        }

        public ApiResult InsertCategory(MainMenu model)
        {
            var result = categoryRepository.Add(model);
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            return ApiResult.ToSuccessModel("ثبت دسته بندی با موفقیت ثبت شد");
        }

        public ApiResult RemoveCategory(int id)
        {
            var category = GetCategory(id);
            if (category == null)
                return ApiResult.ToErrorModel("دسته بندی یافت نشد");

            var result = categoryRepository.Remove(category);
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در حذف دسته بندی");
            return ApiResult.ToSuccessModel("حذف دسته بندی با موفقیت ثبت شد");
        }
    }
}
