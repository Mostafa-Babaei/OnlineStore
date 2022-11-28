using Application.Common;
using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Models;
using infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IOptions<SiteSetting> option;

        public ProductService(
            IMapper mapper, IGenericRepository<Product> productRepository, IOptions<SiteSetting> option)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.option = option;
        }

        public ApiResult changeStateProduct(int productId, bool? newState)
        {
            try
            {
                Product product = GetProduct(productId);
                if (product == null)
                    return ApiResult.ToErrorModel(" محصول یافت نشد");

                if (newState == null)
                    product.IsActive = !product.IsActive;
                else
                    product.IsActive = newState.Value;
                int result = productRepository.SaveEntity();

                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در تغییر وضعیت محصول");
                return ApiResult.ToSuccessModel("تغییر وضعیت محصول با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در تغییر وضعیت محصول");
                return ApiResult.ToErrorModel("خطا در تغییر وضعیت محصول");
            }
        }

        public ApiResult EditProduct(EditProductDto model)
        {
            try
            {
                Product product = GetProduct(model.ProductId);
                if (product == null)
                    return ApiResult.ToErrorModel(" محصول یافت نشد");

                product.Quntity = model.Quntity;
                product.ProductId = model.ProductId;
                product.IsActive = model.IsActive;
                product.CategoryId = model.CategoryId;
                product.BrandId = model.BrandId;
                product.Description = model.Description;
                product.Title = model.Title;

                int result = productRepository.SaveEntity();
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در ویرایش محصول");
                return ApiResult.ToSuccessModel("ویرایش محصول با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ویرایش محصول");
                return ApiResult.ToErrorModel("خطا در ویرایش محصول");
            }
        }

        public ApiResult SaveProduct()
        {
            int result = productRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ویرایش محصول");
            return ApiResult.ToSuccessModel("ویرایش محصول با موفقیت ثبت شد");
        }

        public ApiResult FilterProduct(FilterProductRequestDto requestDto)
        {
            try
            {
                PagingDto<Product> paging = new PagingDto<Product>()
                {
                    Page = requestDto.Page,
                    PageSize = requestDto.Count
                };
                if (requestDto.CategoryFilter > 0)
                {
                    paging = productRepository.GetWithPaging(e => e.CategoryId == requestDto.CategoryFilter, paging);
                }
                else if (requestDto.BrandFilter > 0)
                {
                    paging = productRepository.GetWithPaging(e => e.BrandId == requestDto.BrandFilter, paging);
                }
                else if (!string.IsNullOrEmpty(requestDto.SearchText))
                {
                    paging = productRepository.GetWithPaging(e => e.Title.Contains(requestDto.SearchText), paging);
                }
                else
                {
                    paging = productRepository.GetWithPaging(null, paging);
                }

                //  var result = productRepository.GetWithPaging(e =>
                //(string.IsNullOrEmpty(requestDto.SearchText) && e.Title.Contains(requestDto.SearchText)) &&
                //(requestDto.BrandFilter != null && requestDto.BrandFilter > 0 && e.BrandId == requestDto.BrandFilter) &&
                //(requestDto.CategoryFilte != null && requestDto.CategoryFilte > 0 && e.CategoryId == requestDto.CategoryFilte), paging);

                FilterProductRequestDto resultSearch = new FilterProductRequestDto()
                {
                    SearchText = requestDto.SearchText,
                    BrandFilter = requestDto.BrandFilter,
                    CategoryFilter = requestDto.CategoryFilter,
                    NumberOfPage = paging.TotalPage,
                    TotalCount = paging.TotalCount,
                    Count = paging.PageSize,
                    Page = paging.Page,
                    Products = paging.PageData,
                };
                foreach (var item in resultSearch.Products)
                    item.PicPath = option.Value.ProductImageUrl + item.PicPath;

                return ApiResult.ToSuccessModel("", resultSearch);
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ویرایش محصول");
                return ApiResult.ToErrorModel("خطا در جستجوی محصول");
            }
        }

        //Todo:جستجوی محصول و نمایش تمام محصولات مرتبط با توجه به دسته بندی
        public List<ProductDto> FindProducts(string productName)
        {
            List<ProductDto> products = new List<ProductDto>();
            try
            {
                var productsDto = productRepository.Find(e => e.Title == productName).ToList();
                products = mapper.Map<List<Product>, List<ProductDto>>(productsDto);
                return products;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "");
                return products;
            }
        }

        public List<ProductDto> GetAllProduct()
        {
            List<ProductDto> products = new List<ProductDto>();
            try
            {
                var productsDto = productRepository.GetAll().ToList();
                products = mapper.Map<List<Product>, List<ProductDto>>(productsDto);
                foreach (var item in products)
                    item.PicPath = option.Value.ProductImageUrl + item.PicPath;

                return products;

            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در دریافت محصول");
                return products;
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                return productRepository.Find(e => e.ProductId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در دریافت محصول");
                return null;
            }
        }

        public ProductDto GetProductDto(int id)
        {
            ProductDto product = new ProductDto();
            try
            {
                var productsDto = productRepository.Find(e => e.ProductId == id).FirstOrDefault();
                product = mapper.Map<ProductDto>(productsDto);
                product.PicPath = option.Value.ProductImageUrl + product.PicPath;
                return product;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در دریافت محصول");
                return product;
            }
        }

        public List<ProductDto> GetProductsOfCategory(int categoryId)
        {
            List<ProductDto> products = new List<ProductDto>();
            try
            {
                var productsDto = productRepository.Find(e => e.CategoryId == categoryId).ToList();
                products = mapper.Map<List<Product>, List<ProductDto>>(productsDto);
                return products;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در دریافت محصول");
                return products;
            }
        }

        public ApiResult InsertProduct(InsertProductDto model)
        {
            try
            {
                Product product = mapper.Map<Product>(model);
                productRepository.Add(product);
                int result = productRepository.SaveEntity();
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در ثبت محصول");
                return ApiResult.ToSuccessModel("ثبت محصول با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت محصول");
                return ApiResult.ToErrorModel("خطا در ثبت محصول");
            }
        }

        public ApiResult RemoveProduct(int productId)
        {
            try
            {
                var product = GetProduct(productId);
                int result = productRepository.Remove(product);
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در ثبت محصول");
                return ApiResult.ToSuccessModel("ثبت محصول با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "");
                return ApiResult.ToErrorModel("خطا در ثبت محصول");
            }
        }


    }
}
