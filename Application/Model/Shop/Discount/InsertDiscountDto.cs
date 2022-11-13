using Domain.Entities;
using System;

namespace Application.Model
{
    public class InsertDiscountDto
    {
        public int DiscountId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Amount { get; set; }
        public int Count { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
