using Api.Database;
using Api.Validation;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Movies.UpdateMovie;

public class UpdateMovieCommandHandler(MovieContext context)
    : IRequestHandler<UpdateMovieCommand, Result<string>>
{
    private readonly MovieContext _context = context;

    public async Task<Result<string>> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        var movie = await _context.Movies
            .FirstOrDefaultAsync(movie => movie.Id == command.Id, cancellationToken);

        if (movie == null)
        {
            return Result.Fail<string>(
                new ValidationError(nameof(command.Id), "Nie znaleziono filmu o podanym identyfikatorze."));
        }

        if (string.IsNullOrWhiteSpace(command.Request.Title))
            result.WithError(new ValidationError(nameof(command.Request.Title), "Tytuł jest wymagany."));

        if (string.IsNullOrWhiteSpace(command.Request.Director))
            result.WithError(new ValidationError(nameof(command.Request.Director), "Reżyser jest wymagany."));

        if (command.Request.ReleaseYear == null)
        {
            result.WithError(new ValidationError(nameof(command.Request.ReleaseYear), "Rok wydania jest wymagany."));
        }
        else if (command.Request.ReleaseYear < 1888 || command.Request.ReleaseYear > DateTime.Now.Year + 5)
        {
            result.WithError(new ValidationError(nameof(command.Request.ReleaseYear), "Rok wydania jest nieprawidłowy."));
        }

        if (command.Request.GenreId.HasValue)
        {
            var genreExists = await _context.Genres
                .AnyAsync(genre => genre.Id == command.Request.GenreId.Value, cancellationToken);

            if (!genreExists)
                result.WithError(new ValidationError(nameof(command.Request.GenreId), "Wybrany gatunek nie istnieje."));
        }

        if (command.Request.StatusId.HasValue)
        {
            var statusExists = await _context.Statuses
                .AnyAsync(status => status.Id == command.Request.StatusId.Value, cancellationToken);

            if (!statusExists)
                result.WithError(new ValidationError(nameof(command.Request.StatusId), "Wybrany status nie istnieje."));
        }

        if (result.IsFailed)
            return result;

        var duplicateExists = await _context.Movies
            .AnyAsync(existingMovie =>
                existingMovie.Id != command.Id
                && existingMovie.Title.ToLower() == command.Request.Title!.ToLower()
                && existingMovie.ReleaseYear == command.Request.ReleaseYear,
                cancellationToken);

        if (duplicateExists)
        {
            return Result.Fail<string>(
                new ValidationError(nameof(command.Request.Title), "Film o podanym tytule i roku wydania już istnieje."));
        }

        movie.Title = command.Request.Title!;
        movie.Director = command.Request.Director!;
        movie.ReleaseYear = command.Request.ReleaseYear!.Value;
        movie.GenreId = command.Request.GenreId;
        movie.StatusId = command.Request.StatusId;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok("Zaktualizowano film.");
    }
}