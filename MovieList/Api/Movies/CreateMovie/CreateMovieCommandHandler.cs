using Api.Database;
using Api.Database.Entities;
using Api.Validation;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.CreateMovie;

public class CreateMovieCommandHandler(MovieContext context)
    : IRequestHandler<CreateMovieCommand, Result<string>>
{
    private readonly MovieContext _context = context;

    public async Task<Result<string>> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        if (string.IsNullOrWhiteSpace(command.Request.Title))
            result.WithError(new ValidationError(nameof(command.Request.Title), "Tytuł jest wymagany"));

        if (string.IsNullOrWhiteSpace(command.Request.Director))
            result.WithError(new ValidationError(nameof(command.Request.Director), "Reżyser jest wymagany"));

        if (command.Request.ReleaseYear == null)
            result.WithError(new ValidationError(nameof(command.Request.ReleaseYear), "Rok wydania jest wymagany"));
        else if (command.Request.ReleaseYear < 1888 || command.Request.ReleaseYear > DateTime.Now.Year + 5)
            result.WithError(new ValidationError(nameof(command.Request.ReleaseYear), "Rok wydania jest nieprawidłowy"));

        if (command.Request.GenreId.HasValue)
        {
            var genreExists = await _context.Genres
                .AnyAsync(genre => genre.Id == command.Request.GenreId.Value, cancellationToken);

            if (!genreExists)
                result.WithError(new ValidationError(nameof(command.Request.GenreId), "Wybrany gatunek nie istnieje"));
        }

        if (command.Request.StatusId.HasValue)
        {
            var statusExists = await _context.Statuses
                .AnyAsync(status => status.Id == command.Request.StatusId.Value, cancellationToken);

            if (!statusExists)
                result.WithError(new ValidationError(nameof(command.Request.StatusId), "Wybrany status nie istnieje"));
        }

        if (result.IsFailed)
            return result;

        var movieExists = await _context.Movies
            .AnyAsync(movie =>
                movie.Title.ToLower() == command.Request.Title!.ToLower()
                && movie.ReleaseYear == command.Request.ReleaseYear,
                cancellationToken);

        if (movieExists)
            return Result.Fail<string>(new ValidationError(nameof(command.Request.Title), "Film o podanym tytule i roku wydania już istnieje"));

        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = command.Request.Title!,
            Director = command.Request.Director!,
            ReleaseYear = command.Request.ReleaseYear!.Value,
            GenreId = command.Request.GenreId,
            StatusId = command.Request.StatusId
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok($"Zapisano nowy film. Id: {movie.Id}");
    }
}