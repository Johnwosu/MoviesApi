using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;

namespace MoviesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IDbConnection _db;

    // Constructor
    public MoviesController(IDbConnection db)
    {
        _db = db;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var sql = "SELECT * FROM Movies";

        var movies =
            await _db.QueryAsync<Movie>(sql);

        return Ok(movies);
    }

    // GET: api/movies/2
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDetails>> GetById(int id)
    {
        // Get one movie by its ID.
        var movieSql = """
            SELECT
                Id,
                GenreId,
                AudienceRatingId,
                Title,
                Synopsis,
                RuntimeMinutes,
                TheaterReleaseDate,
                ImageUrl
            FROM Movies
            WHERE Id = @Id
            """;

        var movie =
            await _db.QuerySingleOrDefaultAsync<MovieDetails>(
                movieSql,
                new { Id = id });

        // Return 404 if the movie does not exist.
        if (movie == null)
        {
            return NotFound();
        }

        // Get only published reviews for this movie.
        // Match CreatedBy with UserSnapshot.UserId
        // so the review includes the critic's name.
        var reviewSql = """
            SELECT
                cr.Rating,
                cr.Description,
                cr.CreatedDate,
                us.Name AS CreatorName
            FROM CriticReviews cr
            INNER JOIN UserSnapshot us
                ON cr.CreatedBy = us.UserId
            WHERE cr.MovieId = @Id
                AND cr.IsPublished = 1
            ORDER BY cr.CreatedDate DESC
            """;

        var reviews =
            await _db.QueryAsync<Review>(
                reviewSql,
                new { Id = id });

        movie.Reviews = reviews.ToList();

        return Ok(movie);
    }
}