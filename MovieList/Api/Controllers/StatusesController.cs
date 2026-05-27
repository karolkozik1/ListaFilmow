using Api.Database;
using Api.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusesController(MovieContext context) : ControllerBase
{
    private readonly MovieContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var statuses = await _context.Statuses
            .OrderBy(status => status.Name)
            .Select(status => status.ToStatusDto())
            .ToListAsync(cancellationToken);

        return Ok(statuses);
    }
}