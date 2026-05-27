namespace Common.Movies;

public class CreateMovieRequest
{
    public string? Title { get; set; }

    public string? Director { get; set; }

    public int? ReleaseYear { get; set; }

    public Guid? GenreId { get; set; }

    public int? StatusId { get; set; }
}