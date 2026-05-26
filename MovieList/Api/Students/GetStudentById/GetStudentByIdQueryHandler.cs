using Api.Database;
using Api.Mapping;
using Api.Students.GetStudentsPaged;
using Common.CommonData;
using Common.Students;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Students.GetStudentById;

public class GetStudentByIdQueryHandler(SanContext context) : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly SanContext _context = context;

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var guid = Guid.Parse(request.Id);
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == guid);
        if (student == null)
            return null;  
        
        return student.ToStudentDto();
    }
}
