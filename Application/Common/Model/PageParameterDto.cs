using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Model
{
    public class PageParameterDto
    {
        public int TotalCount { get; set; } = 0;
        public int TotalPage { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
