using Api.Database.Entities;
using Common.Students;

namespace Api.Mapping;

public static class Mapper
{
    public static StudentBasicDto ToStudentBasicDto(this Student student)
    {
        return new StudentBasicDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            AlbumNumber = student.AlbumNumber,
            StatusId = student.StatusId,
            StatusName = student.Status!.Name
        };
    }

    public static StudentDto ToStudentDto(this Student student)
    {
        return new StudentDto
        {
            Id= student.Id,
            FullName = $"{student.LastName} {student.FirstName}",
            AlbumNumber = student.AlbumNumber,
        };
    }
}
