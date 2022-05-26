using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Route("top-grossing")]
        [HttpGet]
        public async Task<IActionResult> TopGrossing()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies found" });
            }
            return Ok(movies);
        }

        [Route("(id)")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movies = await _movieService.GetMovieDetails(id);

            if(movies == null)
            {
                return NotFound(new { errorMessage = "No Movies found" });
            }
            return Ok(movies);
        }

    }
}
