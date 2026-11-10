using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;

namespace MoviesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Fjord"
            },

            new Movie
            {
                Id = 2,
                Title = "The Long Road"
            },

            new Movie
            {
                Id = 3,
                Title = "After Midnight"
            }
        };

        await Task.CompletedTask;

        return Ok(movies);
    }
}