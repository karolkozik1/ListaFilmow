using Api.Students.CreateStudent;
using Api.Students.GetStudentById;
using Api.Students.GetStudentsPaged;
using Api.Validation;
using Azure.Core;
using Common.Students;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;
using Common.CommonData;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(ISender sender, IValidationProblemsHandler handler) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IValidationProblemsHandler _handler = handler;

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request, CancellationToken token)
    {
        var command = new CreateStudentCommand(request);
        var result = await _sender.Send(command, token);
        if (result.IsSuccess)
            return StatusCode((int)HttpStatusCode.Created, result.Value);

        var problemDetails = _handler.Handle(result);
        return BadRequest(problemDetails);
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetPaged([FromQuery] GetStudentsPagedRequest request, CancellationToken token)
    {
        var query = new GetStudentsPagedQuery(request);
        var result = await _sender.Send(query, token);

        return Ok(result);
    }

    [HttpGet]
    [Route("student")]
    [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetById([FromQuery] string id, CancellationToken token)
    {
        var query = new GetStudentByIdQuery(id);
        var result = await _sender.Send(query, token);
        if (result == null)
            return NoContent();

        return Ok(result);
    }
}
