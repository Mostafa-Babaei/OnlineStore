using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Model
{
    public class PagingDto<TEntity>
    {
        public int TotalCount { get; set; } = 0;
        public int TotalPage { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<TEntity> PageData { get; set; }
    }
}
