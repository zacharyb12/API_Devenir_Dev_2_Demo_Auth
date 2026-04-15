using Application_Devenir_Dev_2.Services.Interfaces;
using Domain_Devenir_Dev_2.Entities;
using Domain_Devenir_Dev_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.Services
{
    public class UserService(IUserRepository _repository) : IUserService
    {
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _repository.DeleteUserAsync(id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            User? user = await _repository.GetByIdAsync(id);

            if(user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> UpdateUserAsync(User userToUpdate, int id)
        {
            return await _repository.UpdateUserAsync(userToUpdate, id);
        }
    }
}
