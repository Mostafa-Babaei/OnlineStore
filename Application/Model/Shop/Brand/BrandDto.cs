using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
    }
}
