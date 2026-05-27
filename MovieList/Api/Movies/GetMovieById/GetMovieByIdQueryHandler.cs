using Api.Database;
using Api.Mapping;
using Api.Movies.GetMoviesPaged;
using Common.CommonData;
using Common.Movies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.GetMovieById;

public class GetMovieByIdQueryHandler(MovieContext context) : IRequestHandler<GetMovieByIdQuery, MovieDto?>
{
    private readonly MovieContext _context = context;

    public async Task<MovieDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var guid = Guid.Parse(request.Id);
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == guid);
        if (movie == null)
            return null;  

        return movie.ToMovieDto();
    }
}
