namespace MoviesApi.DTOs;

public class Review
{
    public int Rating { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public string CreatorName { get; set; } = string.Empty;
}