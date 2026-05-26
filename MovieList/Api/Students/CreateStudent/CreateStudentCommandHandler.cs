using Api.Database;
using Api.Database.Entities;
using Api.Validation;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Students.CreateStudent
{
    public class CreateStudentCommandHandler(SanContext context) : IRequestHandler<CreateStudentCommand, Result<string>>
    {
        private readonly SanContext _context = context;

        public async Task<Result<string>> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var result = new Result<string>();
            var albumExists = await _context.Students
                .FirstOrDefaultAsync(s => s.AlbumNumber == command.Request.AlbumNumber);

            if (albumExists is not null)
                result.WithError(new ValidationError(nameof(command.Request.AlbumNumber), "Student o podanym numerze albumu już istnieje"));

            if (result.IsFailed)
                return result;

            var student = new Student
            {
                FirstName = command.Request.FirstName!,
                LastName = command.Request.LastName!,
                AlbumNumber = command.Request.AlbumNumber!
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Result.Ok($"Zapisano nowego studenta. Id: {student.Id}");
        }
    }
}
