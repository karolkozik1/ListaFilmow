using Api.Database;
using Api.Mapping;
using Common.Movies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.GetMovieById;

public class GetMovieByIdQueryHandler(MovieContext context)
    : IRequestHandler<GetMovieByIdQuery, MovieDto?>
{
    private readonly MovieContext _context = context;

    public async Task<MovieDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .Include(movie => movie.Genre)
            .Include(movie => movie.Status)
            .Include(movie => movie.Ratings)
            .FirstOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken);

        return movie?.ToMovieDto();
    }
}