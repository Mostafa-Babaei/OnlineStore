using Application.Common.Model;
using Application.Model;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.Service
{
    public interface IProductService
    {

        /// <summary>
        /// لیست محصولات
        /// </summary>
        /// <returns></returns>
        List<ProductDto> GetAllProduct();

        /// <summary>
        /// دریافت محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product? GetProduct(int id);

        /// <summary>
        /// دریافت مدل محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductDto GetProductDto(int id);

        /// <summary>
        /// افزودن محصول جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult InsertProduct(InsertProductDto model);

        /// <summary>
        /// ویرایش محصول
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult EditProduct(EditProductDto model);

        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ApiResult RemoveProduct(int productId);

        /// <summary>
        /// تغییر وضعیت محصول
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        ApiResult changeStateProduct(int productId, bool? newState);

        /// <summary>
        /// دریافت محصولات یک دسته بندی خاص
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        List<ProductDto> GetProductsOfCategory(int categoryId);

        /// <summary>
        /// جستحوی محصول
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        List<ProductDto> FindProducts(string productName);

        /// <summary>
        /// فیلتر محصولات
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public ApiResult FilterProduct(FilterProductRequestDto requestDto);

    }
}
