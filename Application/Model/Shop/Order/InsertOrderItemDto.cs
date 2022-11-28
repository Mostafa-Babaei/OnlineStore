using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class InsertOrderItemDto
    {
        public int Quantity { get; set; }
        public int BasePrice { get; set; }
        public int TotalPrice { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
