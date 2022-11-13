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
        List<MainMenu> GetAllCategory();
        MainMenu GetCategory(int id);
        ApiResult InsertCategory(MainMenu model);
        ApiResult EditCategory(MainMenu tag);
        ApiResult RemoveCategory(int id);
        bool ExistCategory(string category);
    }
}
