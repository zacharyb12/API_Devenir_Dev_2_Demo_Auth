using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.DTOS
{
    public class LoginForm
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
