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
    // Entity Framework Core
    // Entity Framework Core SQL Server
    // Entity Framework Core Tools
                                   // Injection du Context pour accéder à la base de données
    public class MovieRepository(MovieContext _context) : IMovieRepository
    {

        // Utilisation du contexte pour récupérer les films de la base de données
        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> CreateMovie(Movie newMovie)
        {
           _context.Movies.Add(newMovie);

            await _context.SaveChangesAsync();

            return newMovie;
        }


        // Get By Id
        public async Task<Movie?> GetById(int id)
        {
            Movie? movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if(movie == null)
            {
                return null;
            }

            return movie;
        }

        // Update
        public async Task<Movie?> UpdateMovie(int id , Movie updatedMovie)
        {
            Movie movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if(movieToUpdate == null)
            {
                return null;
            }

            // modification des champs de l'objet

            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.Synopsis = updatedMovie.Synopsis;
            movieToUpdate.Genre = updatedMovie.Genre;
            movieToUpdate.ImageUrl = updatedMovie.ImageUrl;

            // sauvegarde des modifications dans la base de données
            await _context.SaveChangesAsync();

            // retourne une reponse
            return movieToUpdate;
        }

        // Delete
        public async Task<bool> DeleteMovie(int id)
        {
            Movie? movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return false;
                //throw new Exception("Movie not found");
            }

            // execution
            _context.Remove(movie);

            // sauvegarde des modifications dans la base de données
            await _context.SaveChangesAsync();

            // reponse à renvoyer
            return true;
        }
    }
}
