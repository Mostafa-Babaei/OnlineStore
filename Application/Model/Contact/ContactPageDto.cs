using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class ContactPageDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
