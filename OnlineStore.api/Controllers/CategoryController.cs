using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using Domain.Entities;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// دریافت دسته بندی های محصول
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "get")]
        public ApiResult Get()
        {
            try
            {
                return ApiResult.ToSuccessModel(CategoryMessages.ReceivedCategoriesSuccess, categoryService.GetAllCategory());
            }
            catch (Exception)
            {
                return ApiResult.ToErrorModel(CategoryMessages.ReceivedCategoriesFailure);
            }
        }

        /// <summary>
        /// افزودن دسته بندی جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "add")]
        public ApiResult AddCategory([FromBody] InsertCategoryDto categoryMOdel)
        {
            return categoryService.InsertCategory(categoryMOdel);
        }

        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        /// <returns></returns>
        [HttpPut(Name = "edit")]
        public ApiResult EditCategory([FromBody] EditCategoryDto editCategoryDto)
        {
            return categoryService.EditCategory(editCategoryDto);
        }

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(Name = "delete")]
        public ApiResult DeleteCategory(int id)
        {
            return categoryService.RemoveCategory(id);
        }
    }
}
