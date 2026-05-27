namespace Common.Movies;

public class MovieBasicDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }

    public int? StatusId { get; set; }
    public string? StatusName { get; set; }
    public double? AverageRating { get; set; }
    public int? RatingsCount { get; set; }

}