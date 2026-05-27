namespace Common.Ratings;

public class CreateRatingRequest
{
    public Guid MovieId { get; set; }

    public int Score { get; set; }

    public string? Comment { get; set; }
}