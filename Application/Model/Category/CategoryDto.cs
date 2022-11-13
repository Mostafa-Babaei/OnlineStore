using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}
