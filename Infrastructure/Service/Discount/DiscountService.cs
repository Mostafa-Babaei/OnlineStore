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

namespace infrastructure.Service.Discount
{
    public class DiscountService : IDiscountService
    {
        private readonly IGenericRepository<Domain.Entities.Discount> discountRepository;
        private readonly IGenericRepository<UseDiscount> useDiscountRepository;
        private readonly IMapper mapper;

        public DiscountService(IGenericRepository<Domain.Entities.Discount> discountRepository,
            IGenericRepository<UseDiscount> useDiscountRepository,
            IMapper mapper
            )
        {
            this.discountRepository = discountRepository;
            this.useDiscountRepository = useDiscountRepository;
            this.mapper = mapper;
        }
        public ApiResult ChangeStateDiscount(int id)
        {
            var discount = GetDiscountById(id);
            if (discount == null)
                ApiResult.ToErrorModel("تخفیف یافت نشد");
            discount.IsActive = !discount.IsActive;

            int result = discountRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در تغییر وضعیت تخفیف");

            return ApiResult.ToSuccessModel("تغییر وضعیت تخفیف با موفقیت انجام شد");
        }

        public ApiResult CheckDiscount(int id)
        {
            var discounts = GetDiscountById(id);

            if (discounts == null)
                return new ApiResult() { IsSuccess = false, Message = "کد تخفیف وارد شده صحیح نیست" };

            if (!discounts.IsActive || discounts.IsDelete)
                return new ApiResult() { IsSuccess = false, Message = "کد تخفیف وارد شده غیر فعال می باشد" };

            if (discounts.ExpirationDate < DateTime.Now)
                return new ApiResult() { IsSuccess = false, Message = "زمان استفاده از کد تخفیف به پایان رسیده است" };

            int usegeDiscount = GetCountUsedDiscount(discounts.DiscountId);
            if (discounts.Count <= usegeDiscount)
                return new ApiResult() { IsSuccess = false, Message = "سقف استفاده از کد تخفیف به پایان رسیده است" };

            return new ApiResult() { IsSuccess = true, Message = "دریافت تخفیف ها با موفقیت انجام شد" };
        }

        public bool ExistCode(string code)
        {
            return discountRepository.Exist(e => e.Code == code);
        }

        public int GetCountUsedDiscount(int discountId)
        {
            return useDiscountRepository.Find(e => e.DiscountId == discountId && e.IsCancel == false && e.IsDelete == false).Count();
        }

        public DiscountDto GetDiscountByCode(string code)
        {
            var discount = discountRepository.Find(e => e.Code == code).SingleOrDefault();
            return mapper.Map<DiscountDto>(discount);
        }

        public DiscountDto GetDiscountDtoById(int discountId)
        {
            var discount = GetDiscountById(discountId);
            return mapper.Map<DiscountDto>(discount);
        }

        public List<DiscountDto> GetDiscounts()
        {
            var listOfDiscount = discountRepository.Find(e => e.IsDelete == false).ToList();
            return mapper.Map<List<Domain.Entities.Discount>, List<DiscountDto>>(listOfDiscount);
        }

        public List<UseDiscount> GetUseDiscount(int discountId)
        {
            return useDiscountRepository.Find(e => e.DiscountId == discountId).ToList();
        }

        public ApiResult InsertDiscounts(InsertDiscountDto discount)
        {
            var insertDiscount = mapper.Map<Domain.Entities.Discount>(discount);
            discountRepository.Add(insertDiscount);
            int result = discountRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ایجاد تخفیف");

            return ApiResult.ToSuccessModel("ایجاد تخفیف با موفقیت انجام شد");
        }

        public ApiResult RemoveDiscount(int id)
        {
            var discount = GetDiscountById(id);
            if (discount == null)
                ApiResult.ToErrorModel("تخفیف یافت نشد");
            discount.IsDelete = true;

            int result = discountRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در حذف وضعیت تخفیف");

            return ApiResult.ToSuccessModel("حذف تخفیف با موفقیت انجام شد");
        }

        public ApiResult UseDiscount(InsertUseDiscountDto useDiscount)
        {
            var insertDiscount = mapper.Map<UseDiscount>(useDiscount);
            useDiscountRepository.Add(insertDiscount);
            int result = useDiscountRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در استفاده از تخفیف");

            return ApiResult.ToSuccessModel("استفاده از تخفیف با موفقیت انجام شد");
        }

        public Domain.Entities.Discount GetDiscountById(int discountId)
        {
            return discountRepository.Find(e => e.DiscountId == discountId).SingleOrDefault();
        }
    }
}
