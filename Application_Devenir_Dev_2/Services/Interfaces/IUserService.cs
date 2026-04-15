using Application_Devenir_Dev_2.DTOS;
using Domain_Devenir_Dev_2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);

        Task<bool> UpdateUserAsync(User userToUpdate, int id);

        Task<bool> DeleteUserAsync(int id);
    }
}
