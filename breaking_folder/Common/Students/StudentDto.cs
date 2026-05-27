using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Movies;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = "";
    public string AlbumNumber { get; set; } = "";
}
