using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IGenericRepository<ShoppingCart> shoppingCartRepository;
        private readonly IMapper mapper;

        public ShoppingCartService(IGenericRepository<ShoppingCart> shoppingCartRepository, IMapper mapper)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }
        public ApiResult AddToCart(ShoppingCartDto shopingCart)
        {
            try
            {
                ShoppingCart model = new ShoppingCart()
                {
                    Count = shopingCart.Count,
                    UserId = shopingCart.UserId,
                    ProductId = shopingCart.ProductId,
                    Product = shopingCart.Product
                };

                int result = 0;

                if (!CheckExistProductInUserCart(shopingCart.UserId, shopingCart.ProductId))
                {
                    model.CreateAt = DateTime.Now;
                    model.IsEnable = true;
                    shoppingCartRepository.Add(model);
                    result = shoppingCartRepository.SaveEntity();
                    if (result <= 0)
                        return ApiResult.ToErrorModel("خطا در ثبت سبد خرید");
                    return ApiResult.ToSuccessModel("کالا با موفقیت در سبد خرید ثبت شد");
                }
                else
                {
                    ShoppingCart cartItem = GetItemOfCart(shopingCart.UserId, shopingCart.ProductId);
                    if (cartItem == null)
                        return ApiResult.ToErrorModel("کالا در سبد خرید یافت نشد");
                    return IncrementCart(shopingCart, shopingCart.Count);
                }
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            }
        }

        public ApiResult DecrementCart(ShoppingCartDto shopingCart, int count)
        {
            try
            {
                ShoppingCart model = GetItemOfCart(shopingCart.UserId, shopingCart.ProductId);
                if (model == null)
                    return ApiResult.ToErrorModel(" آیتم یافت نشد");

                model.Count -= count;
                int result = shoppingCartRepository.SaveEntity();
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در کاهش تعداد کالا در  سبد خرید");
                return ApiResult.ToSuccessModel("تعداد کالا با موفقیت کاهش یافت");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            }
        }

        public ApiResult DeleteCart(string userId)
        {
            try
            {
                List<ShoppingCart> carts = shoppingCartRepository.Find(e => e.UserId == userId).ToList();
                if (carts == null || !carts.Any())
                    return ApiResult.ToErrorModel("سبد خرید خالی است");

                int result = shoppingCartRepository.RemoveRange(carts);
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در حذف کالاهای سبد خرید");
                return ApiResult.ToSuccessModel("کالاهای سبد خریدا با موفقیت حذف شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            }
        }

        public bool ExistCartForUser(string userId)
        {
            return shoppingCartRepository.Exist(e => e.UserId == userId);
        }

        public ShoppingCart GetItemOfCart(string userId, int productId)
        {
            try
            {
                return shoppingCartRepository.Find(e => e.UserId == userId && e.ProductId == productId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return new ShoppingCart();
            }
        }


        public ApiResult RemoveItemOfCart(string userId, int productId)
        {
            try
            {
                ShoppingCart item = shoppingCartRepository.Find(e => e.UserId == userId && e.ProductId == productId).FirstOrDefault();
                if (item == null)
                    return ApiResult.ToErrorModel("کالا در سبد خرید یافت نشد");

                int result = shoppingCartRepository.Remove(item);

                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در حذف آیتم سبد خرید");

                return ApiResult.ToSuccessModel("آیتم مورد نظر حذف شد");

            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در حذف");
            }
        }

        public List<ShoppingCartDto> GetItemsOfCustomer(string userId)
        {
            List<ShoppingCartDto> carts = new List<ShoppingCartDto>();
            try
            {
                var model = shoppingCartRepository.Find(e => e.UserId == userId).ToList();
                return mapper.Map<List<ShoppingCart>, List<ShoppingCartDto>>(model);
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return carts;
            }
        }

        public ApiResult IncrementCart(ShoppingCartDto shopingCart, int count)
        {
            try
            {
                ShoppingCart model = GetItemOfCart(shopingCart.UserId, shopingCart.ProductId);
                if (model == null)
                    return ApiResult.ToErrorModel(" آیتم یافت نشد");
                model.Count += count;
                int result = shoppingCartRepository.SaveEntity();
                if (result <= 0)
                    return ApiResult.ToErrorModel("خطا در افزایش تعداد کالا در  سبد خرید");
                return ApiResult.ToSuccessModel("تعداد کالا با موفقیت اضافه شد");
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "خطا در ثبت دسته بندی");
                return ApiResult.ToErrorModel("خطا در ثبت دسته بندی");
            }
        }

        private bool CheckExistProductInUserCart(string userId, int productId)
        {
            return shoppingCartRepository.Exist(e => e.UserId == userId && e.ProductId == productId);
        }

        public ApiResult ChangeNumberOfItem(string userId, int productId, int count)
        {
            ShoppingCart shoppingCart = GetItemOfCart(userId, productId);
            if (shoppingCart == null)
                return ApiResult.ToErrorModel("محصول یافت نشد");
            shoppingCart.Count = count;
            int result = shoppingCartRepository.SaveEntity();

            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در تغییر تعداد کالا در  سبد خرید");
            return ApiResult.ToSuccessModel("تعداد کالا با موفقیت تغییر کرد");
        }

    }
}
