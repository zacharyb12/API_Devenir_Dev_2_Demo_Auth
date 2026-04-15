using Application_Devenir_Dev_2.DTOS;
using Application_Devenir_Dev_2.Services.Interfaces;
using Application_Devenir_Dev_2.Tools;
using Domain_Devenir_Dev_2.Entities;
using Domain_Devenir_Dev_2.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.Services
{
    public class AccountService(IUserRepository _repository, IOptions<JwtSettings> _jwtOptions ) : IAccountService
    {
        public async Task<string> Login(LoginForm form)
        {
            User? user = await _repository.GetByEmailAsync(form.Login);

            if(user == null)
            {
                return null;
            }

            // verifie le mot de passe
            if(!BCrypt.Net.BCrypt.Verify(form.Password, user.PasswordHash))
            {
                return null;
            }

            // genere le token
            JwtHelpers helpers = new JwtHelpers(_jwtOptions);

            string token = helpers.GenerateJwtToken(user.Email,user.Role);

            return token;
        }

        public async Task<string> Register(RegisterForm form)
        {
            User? user = await _repository.GetByEmailAsync(form.Email);

            if (user != null)
            {
                return null;
            }

            User userToAdd = new()
            {
                Username = form.Username,
                Email = form.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(form.Password)
            };

            User? result = await _repository.CreateUserAsync(userToAdd);

            if( result == null)
            {
                return null;
            }

            JwtHelpers helpers = new JwtHelpers(_jwtOptions);

            string token = helpers.GenerateJwtToken(result.Email, result.Role);

            return token;
        }
    }
}
