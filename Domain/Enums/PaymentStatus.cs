using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "انصراف")]
        WaitForPaid = 0,

        [Display(Name = "تائید")]
        AcceptPaid = 1,

        [Display(Name = "تائید شده")]
        Verified = 2
    }
}
