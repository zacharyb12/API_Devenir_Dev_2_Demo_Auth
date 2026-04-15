using Domain_Devenir_Dev_2.Entities;
using Domain_Devenir_Dev_2.Interfaces;
using Infrastructure.Devenir_Dev_2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Devenir_Dev_2.Repositories
{
    public class UserRepository(MovieContext _context) : IUserRepository
    {
        // GetById
        public async Task<User?> GetByIdAsync(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        // GetByEmail
        public async Task<User?> GetByEmailAsync(string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                return null;
            }

            return user;
        }
        // CreateUser
        public async Task<User?> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if(user.Id == 0)
            {
                return null;
            }

            return user;
        }

        // UpdateUser
        public async Task<bool> UpdateUserAsync(User userToUpdate, int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                return false;
            }

            user.Username = userToUpdate.Username;
            user.Email = userToUpdate.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        // DeleteUser
        public async Task<bool> DeleteUserAsync(int id)
        {
            // Verifier si un utilisateur existe avec cet id
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            // si non mettre fin à l'operation
            if(user == null)
            {
                return false;
            }

            // si oui le supprimer de la base de données
            _context.Users.Remove(user);

            // sauvegarder les modifications dans la base de données
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
