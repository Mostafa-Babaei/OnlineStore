using Application.Common.Model;
using Application.Model;
using Domain.Entities;
using System.Collections.Generic;

namespace infrastructure.Service
{
    public interface IDiscountService
    {
        List<DiscountDto> GetDiscounts();
        ApiResult InsertDiscounts(InsertDiscountDto discount);

        /// <summary>
        /// دریافت اطلاعات مدل کد تخفیف با آی دی
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        DiscountDto GetDiscountDtoById(int discountId);

        /// <summary>
        /// دریافت اطلاعات کد تخفیف با آی دی
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        Domain.Entities.Discount GetDiscountById(int discountId);

        /// <summary>
        /// بررسی موجود بودن کد تخفبف
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ExistCode(string code);

        /// <summary>
        /// دریافت اطلاعات تخفیف با کد
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        DiscountDto GetDiscountByCode(string code);

        /// <summary>
        /// تعداد استفاده از کد تخفیف
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        int GetCountUsedDiscount(int discountId);

        /// <summary>
        /// بررسی وضعیت کد تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult CheckDiscount(int id);

        /// <summary>
        /// حذف کد تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult RemoveDiscount(int id);

        /// <summary>
        /// تغییر وضعیت کد تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult ChangeStateDiscount(int id);

        /// <summary>
        /// استفاده از کد تخفیف
        /// </summary>
        /// <param name="useDiscount"></param>
        /// <returns></returns>
        ApiResult UseDiscount(InsertUseDiscountDto useDiscount);

        /// <summary>
        /// دریافت مصرف کننده های کد تخفیف
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        List<UseDiscount> GetUseDiscount(int discountId);
    }
}
