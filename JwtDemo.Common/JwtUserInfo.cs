using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.Common
{
    public class JwtUserInfo
    {
        //test
        public long Uid { get; set; }

        public string? Role { get; set; }

        public string? Work { get; set; }

        public string? Email { get; set; }
    }
}
