using Common.Movies;
using FluentResults;
using MediatR;

namespace Api.Students.CreateStudent;

public record CreateStudentCommand(CreateStudentRequest Request) : IRequest<Result<string>>;

