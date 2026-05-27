namespace Common.Movies;

public class UpdateMovieRequest
{
    public string? Title { get; set; }

    public string? Director { get; set; }

    public int? ReleaseYear { get; set; }

    public Guid? GenreId { get; set; }

    public int? StatusId { get; set; }
}