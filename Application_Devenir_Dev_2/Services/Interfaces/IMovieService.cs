using Domain_Devenir_Dev_2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Devenir_Dev_2.Services.Interfaces
{
    public interface IMovieService
    {
        // Get All
        Task<List<Movie>> GetAllAsync();

        // Get By Id
        Task<Movie?> GetByIdAsync(int id);

        // Create
        Task<Movie> CreateMovieAsync(Movie newMovie);

        // Update
        Task<Movie?> UpdateMovie(int id , Movie updatedMovie);

        // Delete
        Task<bool> DeleteMovieAsync(int id);
    }
}
