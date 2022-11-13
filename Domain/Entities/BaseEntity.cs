using System;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public bool IsDelete { get; set; } = false;
        public DateTime CreateAt { get; set; } 
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
