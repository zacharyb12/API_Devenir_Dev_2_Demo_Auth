using Domain_Devenir_Dev_2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Devenir_Dev_2.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> CreateUserAsync(User user);

        Task<bool> UpdateUserAsync(User userToUpdate , int id);

        Task<bool> DeleteUserAsync(int id);
    }
}
