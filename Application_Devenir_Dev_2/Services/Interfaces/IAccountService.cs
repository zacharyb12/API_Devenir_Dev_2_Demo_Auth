using Application_Devenir_Dev_2.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> Login(LoginForm form);

        Task<string> Register(RegisterForm form);
    }
}
