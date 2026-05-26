using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Api.Database.Entities;

[Table("Students")]
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("Id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("FirstName")]
    [NotNull]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Column("LastName")]
    [NotNull]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Column("AlbumNumber")]
    [NotNull]
    [StringLength(6)]
    public string AlbumNumber { get; set; } = string.Empty;

    [ForeignKey("StatusId")]
    public int StatusId { get; set; } = 1;

    public virtual Status? Status { get; set; }

    public virtual ICollection<Course>? Courses { get; set; }
    public virtual ICollection<Note>? Notes { get; set; }
}
