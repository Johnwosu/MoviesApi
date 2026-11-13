namespace MoviesApi.DTOs;

public class MovieDetails
{
    public int Id { get; set; }

    public int GenreId { get; set; }

    public int AudienceRatingId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Synopsis { get; set; } = string.Empty;

    public int RuntimeMinutes { get; set; }

    public DateTime TheaterReleaseDate { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public List<Review> Reviews { get; set; } = new List<Review>();
}