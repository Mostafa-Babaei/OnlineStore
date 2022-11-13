using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class AddNoticeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
    }
}
