namespace Api.Database.Entities;

public class Rating
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int Score { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}