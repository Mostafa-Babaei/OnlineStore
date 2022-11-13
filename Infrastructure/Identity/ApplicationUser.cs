using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public DateTime RegisterAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Mobile { get; set; }
        public string Avatar { get; set; }
        public string NationalCode { get; set; }
        public bool IsActive { get; set; }
    }
}
