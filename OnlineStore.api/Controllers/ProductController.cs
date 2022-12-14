using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using Domain.Models;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
        /// فیلتر محصولات
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult GetProductsByFilter(FilterProductRequestDto requestDto)
        {
            return  productService.FilterProduct(requestDto);
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
        public ApiResult AddProduct([FromBody] InsertProductDto insertProduct)
        {

            //string formatFile = System.IO.Path.GetExtension(insertProduct.ProductImage.FileName);
            //string newName = Guid.NewGuid().ToString() + formatFile;
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Product/", newName);

            //insertProduct.PicPath = newName;
            var addresult = productService.InsertProduct(insertProduct);

            //if (addresult.IsSuccess)
            //{
            //    using (var stream = new FileStream(path, FileMode.Create))
            //    {
            //        insertProduct.ProductImage.CopyTo(stream);
            //    }
            //}

            return addresult;
        }


        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ApiResult EditProduc([FromBody] EditProductDto editProducDto)
        {
            if (editProducDto.ProductImage != null && editProducDto.ProductImage.Length > 0)
            {

                string formatFile = System.IO.Path.GetExtension(editProducDto.ProductImage.FileName);
                string newName = Path.GetFileNameWithoutExtension(editProducDto.PicPath) + formatFile;
                //delete old file 
                if (!String.IsNullOrEmpty(editProducDto.PicPath))
                {
                    string pathimg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Product/", editProducDto.PicPath);
                    if (System.IO.File.Exists(pathimg))
                        System.IO.File.Delete(pathimg);
                }

                //copy new file
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Product/", newName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    editProducDto.ProductImage.CopyTo(stream);
                }
                editProducDto.PicPath = newName;
            }

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

        public class uploadModel
        {
            public int Id { get; set; }
            public IFormFile ProductImage { get; set; }
        }
        [HttpPost]
        [Route("{id}")]
        public ApiResult UploadImageOfProduct(int id,IFormFile ProductImage)
        {
            try
            {
                if (ProductImage == null)
                    return ApiResult.ToErrorModel("تصویر یافت نشد");

                string formatFile = System.IO.Path.GetExtension(ProductImage.FileName);
                string newName = Guid.NewGuid().ToString() + formatFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Product/", newName);

                Product product = productService.GetProduct(id);
                if (product == null)
                    return ApiResult.ToErrorModel("محصول یافت نشد");
                string oldImage = product.PicPath;
                product.PicPath = newName;
                var addresult = productService.SaveProduct();

                if (addresult.IsSuccess)
                {
                    //delete old image
                    if (!string.IsNullOrEmpty(oldImage))
                    {
                        string pathimg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Product/", oldImage);
                        if (System.IO.File.Exists(pathimg))
                            System.IO.File.Delete(pathimg);
                    }


                    //add newImage
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ProductImage.CopyTo(stream);
                    }
                }

                return ApiResult.ToSuccessModel("تصویر محصول با موفقیت بارگذاری شد");

            }
            catch (Exception)
            {
                return ApiResult.ToErrorModel("خطا پیش بینی نشده در بارگذاری تصویر محصول");
            }
        }
    }
}
