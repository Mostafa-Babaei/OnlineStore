using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Model;

namespace Application.Model
{
    public class SearchContact : PageParameterDto
    {
        public List<ContactPageDto> Contacts { get; set; }
    }
}
