using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "فارسی")]
        Persion = 0,

        [Display(Name = "English")]
        English = 1,

        [Display(Name = "العربیه")]
        Arabic = 2
    }
}
