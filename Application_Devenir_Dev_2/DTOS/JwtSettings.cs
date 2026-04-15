using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.DTOS
{
    public class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get;set;  }
        public required string Audience { get; set;  }

         
    }
}
