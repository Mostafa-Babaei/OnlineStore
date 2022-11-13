using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum OrderState
    {
        [Display(Name = "ثبت سفارش")]
        DefineOrder = 0,

        [Display(Name = "لغو سفارش")]
        CancelOrder = 1,

        [Display(Name = "تائید سفارش")]
        AcceptOrder = 2,

        [Display(Name = "در انتظار پرداخت")]
        AwaitingPayment = 3,

        [Display(Name = "در انتظار مجری")]
        AwaitingOperator = 4,

        [Display(Name = "معلق")]
        SuspendedOrder = 5,

        [Display(Name = "در حال اجرا")]
        InProgress = 6,

        [Display(Name = "اتمام")]
        Done = 7
    }
}
