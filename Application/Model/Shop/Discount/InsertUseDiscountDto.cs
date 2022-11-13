using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class InsertUseDiscountDto
    {
        public DateTime UseDate { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }

        public bool IsCancel { get; set; }

        public int DiscountId { get; set; }
    }
}
