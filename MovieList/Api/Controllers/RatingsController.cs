using Api.Database;
using Api.Database.Entities;
using Common.Ratings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingsController(MovieContext context) : ControllerBase
{
    private readonly MovieContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRatingRequest request, CancellationToken cancellationToken)
    {
        if (request.Score < 1 || request.Score > 10)
            return BadRequest("Ocena musi być liczbą od 1 do 10.");

        var movieExists = await _context.Movies
            .AnyAsync(movie => movie.Id == request.MovieId, cancellationToken);

        if (!movieExists)
            return NotFound("Nie znaleziono filmu o podanym identyfikatorze.");

        var rating = new Rating
        {
            Id = Guid.NewGuid(),
            MovieId = request.MovieId,
            Score = request.Score,
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, "Dodano ocenę filmu.");
    }
}