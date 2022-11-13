using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class EditPaymentDto
    {
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        public int InvoiceDate { get; set; }
        public string Bank { get; set; }
        public string InvoceNumber { get; set; }
        public string Token { get; set; }
        public string StateCallBack { get; set; }
        public string RefId { get; set; }
        public int Status { get; set; }
        public int EntityType { get; set; }
        public int EntityId { get; set; }
        public bool Paid { get; set; } = false;
        public string CardNumber { get; set; }
        public int BankId { get; set; }
        public virtual Order Order { get; set; }
    }
}
