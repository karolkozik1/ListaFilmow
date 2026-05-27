using Common.CommonData;
using Common.Movies;
using MediatR;

namespace Api.Students.GetStudentById;

public record GetStudentByIdQuery(string Id) : IRequest<StudentDto?>;

