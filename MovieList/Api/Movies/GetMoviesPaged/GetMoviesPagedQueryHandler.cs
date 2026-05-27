using Api.Database;
using Api.Database.Entities;
using Api.Extensions;
using Api.Mapping;
using Common.CommonData;
using Common.Movies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.GetMoviesPaged;

public class GetMoviesPagedQueryHandler(MovieContext context)
    : IRequestHandler<GetMoviesPagedQuery, PagedList<MovieDto>>
{
    private readonly MovieContext _context = context;

    public async Task<PagedList<MovieDto>> Handle(GetMoviesPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Movies
            .Include(movie => movie.Genre)
            .Include(movie => movie.Status)
            .Include(movie => movie.Ratings)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Request.TitleOrYear))
        {
            var search = request.Request.TitleOrYear.Trim();
            var searchLower = search.ToLower();

            if (int.TryParse(search, out var year))
            {
                query = query.Where(movie => movie.ReleaseYear == year);
            }
            else
            {
                query = query.Where(movie =>
                    movie.Title.ToLower().Contains(searchLower) ||
                    movie.Director.ToLower().Contains(searchLower));
            }
        }

        var data = query
            .OrderBy(movie => movie.Title)
            .ThenBy(movie => movie.ReleaseYear)
            .Select(movie => movie.ToMovieDto());

        return await data.ToPagedListAsync(
            request.Request.PageNumber,
            request.Request.PageSize);
    }
}