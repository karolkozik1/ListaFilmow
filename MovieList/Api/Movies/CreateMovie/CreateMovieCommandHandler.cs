using Api.Database;
using Api.Database.Entities;
using Api.Validation;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.CreateMovie;

public class CreateMovieCommandHandler(MovieContext context) : IRequestHandler<CreateMovieCommand, Result<string>>
{
    private readonly MovieContext _context = context;

    public async Task<Result<string>> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        var movieExists = await _context.Movies
            .AnyAsync(m =>
                m.Title.ToLower() == command.Request.Title!.ToLower()
                && m.ReleaseYear == command.Request.ReleaseYear,
                cancellationToken);

        if (movieExists)
            result.WithError(new ValidationError(nameof(command.Request.Title), "Film o podanym tytule i roku wydania już istnieje"));

        if (result.IsFailed)
            return result;

        var movie = new Movie
        {
            Title = command.Request.Title!,
            Director = command.Request.Director!,
            ReleaseYear = command.Request.ReleaseYear!.Value
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok($"Zapisano nowy film. Id: {movie.Id}");
    }
}