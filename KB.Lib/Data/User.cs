using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KB.Lib.Data
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
