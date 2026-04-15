using Domain_Devenir_Dev_2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Devenir_Dev_2.Interfaces
{
    // La couche domain est la relation entre la couche Infrastructure et la couche Application
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();

        Task<Movie> CreateMovie(Movie newMovie);

        // Get By Id
        Task<Movie?> GetById(int id);

        // Update

        Task<Movie?> UpdateMovie(int id, Movie updatedMovie);

        // Delete
        Task<bool> DeleteMovie(int id);
    }
}
