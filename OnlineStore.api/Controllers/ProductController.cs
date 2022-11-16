using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        /// <summary>
        /// دریافت محصولات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAll()
        {
            return ApiResult.ToSuccessModel(ProductMessages.ReceivedProductsSuccess, productService.GetAllProduct());
        }

        /// <summary>
        /// دریافت محصول
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Get(int id)
        {
            return ApiResult.ToSuccessModel(ProductMessages.ReceivedProductsSuccess, productService.GetProductDto(id));
        }

        /// <summary>
        /// افزودن محصول جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddProduc([FromBody] InsertProductDto insertProduct)
        {
            return productService.InsertProduct(insertProduct);
        }


        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ApiResult EditProduc([FromBody] EditProductDto editProducDto)
        {
            return productService.EditProduct(editProducDto);
        }

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteProduct(int id)
        {
            return productService.RemoveProduct(id);
        }

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult ChangeStateOfProduct(int id, bool? newState)
        {
            return productService.changeStateProduct(id, newState);
        }

    }
}
