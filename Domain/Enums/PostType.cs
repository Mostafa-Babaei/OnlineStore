using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum PostType
    {
        [Display(Name = "همه دسته ها")]
        All = 0,
        [Display(Name = "مطالب آموزشی")]
        Learning = 1,
        [Display(Name = "اخبار")]
        News = 2
    }
}
