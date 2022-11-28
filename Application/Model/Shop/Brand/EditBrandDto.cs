using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class EditBrandDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public IFormFile ImageOfBrand { get; set; }
        public bool IsActive { get; set; }
    }
}
