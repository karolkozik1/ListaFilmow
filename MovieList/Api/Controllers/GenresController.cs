using Api.Database;
using Api.Mapping;
using Common.Genres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController(MovieContext context) : ControllerBase
{
    private readonly MovieContext _context = context;

    [HttpGet]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var genres = await _context.Genres
            .OrderBy(genre => genre.Name)
            .Select(genre => genre.ToGenreDto())
            .ToListAsync(cancellationToken);

        return Ok(genres);
    }
}