using Api.Database;
using Api.Extensions;
using Api.Mapping;
using Common.CommonData;
using Common.Movies;
using MediatR;

namespace Api.Movies.GetMoviesPaged;

public class GetMoviesPagedQueryHandler(MovieContext context) : IRequestHandler<GetMoviesPagedQuery, PagedList<MovieDto>>
{
    private readonly MovieContext _context = context;

    public async Task<PagedList<MovieDto>> Handle(GetMoviesPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Request.TitleOrYear))
        {
            var search = request.Request.TitleOrYear.Trim();

            if (int.TryParse(search, out var year))
            {
                query = query.Where(m => m.ReleaseYear == year);
            }
            else
            {
                query = query.Where(m => m.Title.ToLower().Contains(search.ToLower()));
            }
        }

        var data = query
            .OrderBy(m => m.Title)
            .ThenBy(m => m.ReleaseYear)
            .Select(m => m.ToMovieDto());

        return await data.ToPagedListAsync(request.Request.PageNumber, request.Request.PageSize);
    }
}