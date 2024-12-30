using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;
using System.IO;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // GET all movies
        [HttpGet]
        public ActionResult<List<Movie>> Get() =>
            _movieService.GetMovies();

        // POST a new movie with file upload
        [HttpPost]
        public async Task<ActionResult<Movie>> Post([FromForm] Movie movie, [FromForm] IFormFile poster)
        {
            if (poster != null && poster.Length > 0)
            {
                // Save the poster file to a specific folder (e.g., "uploads")
                var filePath = Path.Combine("uploads", poster.FileName);

                // Ensure the "uploads" folder exists
                Directory.CreateDirectory("uploads");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    // Copy the poster file to the specified file path
                    await poster.CopyToAsync(stream);
                }

                // You can now save the file path in the movie object or database
                movie.PosterUrl = filePath; // Assuming you have a property in the Movie model to store the file path
            }

            // Add the movie to the database
            var addedMovie = _movieService.AddMovie(movie);

            // Return the created movie in the response
            return CreatedAtAction(nameof(Get), new { id = addedMovie.Id }, addedMovie);
        }
    }
}
