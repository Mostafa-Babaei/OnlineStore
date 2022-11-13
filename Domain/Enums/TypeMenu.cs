using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum TypeMenu
    {
        [Display(Name = "منوی اصلی سایت")]
        Main = 0,
        [Display(Name = "منوی پنل مدیریت")]
        Admin = 1,
    }
}
