using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicPath { get; set; }
        public bool IsActive { get; set; }
        public int Quntity { get; set; }
        public long Price { get; set; }
    }
}
