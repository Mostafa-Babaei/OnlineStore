using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model.Payment
{
    public interface IPaymentRequest
    {
        /// <summary>
        /// کد یکتای پرداخت که برای بعضی از بانک ها نیاز هست.
        /// در حال حاضر، برای سفارشات این کد یه کد تولید شده هست و شناسه سفارش نیست.
        /// باید یکتا باشه
        /// </summary>
        long UniqueId { get; set; }

        /// <summary>
        /// مبلغ پرداختی به ریال
        /// </summary>
        int Amount { set; get; }

        /// <summary>
        /// آدرس برگشت از درگاه
        /// </summary>
        string CallbackUrl { get; set; }

        /// <summary>
        /// تسهیمی بودن فاکتور
        /// </summary>
        bool IsShared { get; set; }


    }
}
