using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
