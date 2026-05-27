using Common.CommonData;

namespace Common.Movies;

public class GetMoviesPagedRequest : PagedRequest
{
    public string? TitleOrYear { get; set; }
}