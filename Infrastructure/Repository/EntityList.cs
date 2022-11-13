using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.Repository
{
    public class PagingDto1s<TEntity>
    {
        public int TotalCount { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public IEnumerable<TEntity> PageData { get; set; }
    }
   
}
