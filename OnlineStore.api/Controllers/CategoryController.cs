using Application.Common.Model;
using Application.Constant.Message;
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "add")]
        public ApiResult AddCategory()
        {
            return ApiResult.ToSuccessModel(CommonMessage.AddedDataSuccess);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut(Name = "edit")]
        public ApiResult EditCategory()
        {
            return ApiResult.ToSuccessModel(CommonMessage.EditedDataSuccess);
        }

        [HttpDelete(Name = "delete")]
        public ApiResult DeleteCategory()
        {
            return ApiResult.ToSuccessModel(CommonMessage.DeletedDataSuccess);
        }
    }
}
