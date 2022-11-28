using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BasePrice { get; set; }
        public int TotalPrice { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int OrderId { get; set; }
    }
}
