using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    /// <summary>
    /// مدل دسته بندی
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}
