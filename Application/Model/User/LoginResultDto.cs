using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public List<string> Roles{ get; set; }
    }
}
