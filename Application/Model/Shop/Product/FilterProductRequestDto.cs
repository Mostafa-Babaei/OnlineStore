using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class FilterProductRequestDto
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int NumberOfPage { get; set; }
        public int TotalCount { get; set; }
        public List<Product> Products { get; set; }
        public int? BrandFilter { get; set; }
        public int? CategoryFilte { get; set; }
        public string SearchText { get; set; }
    }
}
