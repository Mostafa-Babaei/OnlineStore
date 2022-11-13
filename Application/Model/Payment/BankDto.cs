using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class BankDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; } = false;
        public string AccountInfo { get; set; }
    }
}
