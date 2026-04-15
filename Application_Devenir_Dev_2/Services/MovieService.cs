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
                                // Injection de la couche Domain pour accéder au données
    public class MovieService(IMovieRepository _repository) : IMovieService
    {

        // On demande l'information à la coucher Domain 
        public async Task<List<Movie>> GetAllAsync()
        {
            return await _repository.GetMovies();
        }

        public async Task<Movie> CreateMovieAsync(Movie newMovie)
        {
            return await _repository.CreateMovie(newMovie);
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<Movie?> UpdateMovie(int id, Movie updatedMovie)
        {
            return await _repository.UpdateMovie(id,updatedMovie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _repository.DeleteMovie(id);
        }

    }
}
