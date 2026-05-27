using Api.Movies.CreateMovie;
using Api.Movies.GetMoviesPaged;
using Api.Movies.GetMovieById;
using Api.Validation;
using Azure.Core;
using Common.Movies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;
using Common.CommonData;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController(ISender sender, IValidationProblemsHandler handler) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IValidationProblemsHandler _handler = handler;

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken token)
    {
        var command = new CreateMovieCommand(request);
        var result = await _sender.Send(command, token);
        if (result.IsSuccess)
            return StatusCode((int)HttpStatusCode.Created, result.Value);

        var problemDetails = _handler.Handle(result);
        return BadRequest(problemDetails);
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetPaged([FromQuery] GetMoviesPagedRequest request, CancellationToken token)
    {
        var query = new GetMoviesPagedQuery(request);
        var result = await _sender.Send(query, token);

        return Ok(result);
    }

    [HttpGet]
    [Route("movie")]
    [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetById([FromQuery] string id, CancellationToken token)
    {
        var query = new GetMovieByIdQuery(id);
        var result = await _sender.Send(query, token);
        if (result == null)
            return NoContent();

        return Ok(result);
    }
}
