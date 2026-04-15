using Application_Devenir_Dev_2.Services.Interfaces;
using Domain_Devenir_Dev_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Devenir_Dev_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
                                // Injection de la couche Application pour accéder au données
    public class MovieController(IMovieService _service) : ControllerBase
    {
        // Le controlleur Renvoie les informations et ou message d'erreur
        #region Get All Movies
        /// <summary>
        /// Return a list of movies
        /// </summary>
        /// <returns> List of string </returns>
        // Le verbe permet à l'application de savoir ou rediriger la requete
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            List<Movie> movies = await _service.GetAllAsync();

            if(movies.Count < 1)
            {
                return NotFound("Aucun films à afficher");
            }

            return Ok(movies);
        }
        #endregion


        #region CreateMovie
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateMovie(Movie newMovie)
        {
            try
            {
                Movie response = await _service.CreateMovieAsync(newMovie);

                if (response == null)
                {
                    return BadRequest("Erreur lors de la création");
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Get Movie By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Movie? movie = await _service.GetByIdAsync(id);

            if(movie == null)
            {
                return NotFound($"Aucun film avec l'id {id}");
            }

            return Ok(movie);
        }

        #endregion


        #region Update Movie
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(int id , Movie updatedMovie)
        {
            try
            {
                 

                if (id < 0)
                {
                    return BadRequest("L'identifiant est incorrect");
                }

                if (updatedMovie.Title == string.Empty)
                {
                    return BadRequest("Le titre est obligatoire");
                }

                // recuperation ou execution 
                Movie? result = await _service.UpdateMovie(id, updatedMovie);

                if (result == null)
                {
                    return NotFound();
                }

                // retourne une reponse
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                bool result = await _service.DeleteMovieAsync(id);

                if(!result)
                {
                    return NotFound();
                }

                return Ok("Le film a bien été supprimé");

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}