using Common.CommonData;
using Common.Movies;
using FluentResults;
using MediatR;

namespace Api.Students.GetStudentsPaged;

public record GetStudentsPagedQuery(GetStudentsPagedRequest Request) : IRequest<PagedList<StudentBasicDto>>;

