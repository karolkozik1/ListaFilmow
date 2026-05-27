namespace Common.Movies;

public class StudentBasicDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string AlbumNumber { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string? StatusName { get; set; }
    public string FullName => $"{LastName} {FirstName}";
}
