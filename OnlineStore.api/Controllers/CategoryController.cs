using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using Domain.Entities;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpGet]
        public ApiResult Get()
        {
            try
            {
                return ApiResult.ToSuccessModel(CategoryMessages.ReceivedCategoriesSuccess, categoryService.GetAllCategory());
            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel(CategoryMessages.ReceivedCategoriesFailure, exception: ex.ToString());
            }
        }

        /// <summary>
        /// افزودن دسته بندی جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddCategory([FromBody] InsertCategoryDto categoryMOdel)
        {
            return categoryService.InsertCategory(categoryMOdel);
        }

        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ApiResult EditCategory([FromBody] EditCategoryDto editCategoryDto)
        {
            return categoryService.EditCategory(editCategoryDto);
        }

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteCategory(int id)
        {
            return categoryService.RemoveCategory(id);
        }
    }
}
