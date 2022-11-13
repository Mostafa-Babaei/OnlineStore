using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Contact : BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsRead { get; set; }
        public bool ShowInHome { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
