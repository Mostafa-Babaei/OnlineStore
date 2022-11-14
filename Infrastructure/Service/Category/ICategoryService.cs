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
        List<Category> GetAllCategory();
        Category GetCategory(int id);
        ApiResult InsertCategory(Category model);
        ApiResult EditCategory(Category tag);
        ApiResult RemoveCategory(int id);
        bool ExistCategory(string category);
    }
}
