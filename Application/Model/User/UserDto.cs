using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class UserDto
    {
        public string userId { get; set; }
        public string fullname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string nationalCode { get; set; }
        public bool isActive { get; set; }
        public List<SelectListItem> role { get; set; }
    }
}
