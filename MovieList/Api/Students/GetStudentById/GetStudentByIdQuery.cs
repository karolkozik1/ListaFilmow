using Common.CommonData;
using Common.Students;
using MediatR;

namespace Api.Students.GetStudentById;

public record GetStudentByIdQuery(string Id) : IRequest<StudentDto?>;

