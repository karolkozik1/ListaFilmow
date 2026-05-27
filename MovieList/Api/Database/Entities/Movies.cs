using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Api.Database.Entities;

[Table("Movies")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("Id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("Title")]
    [NotNull]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Column("Director")]
    [NotNull]
    [StringLength(200)]
    public string Director { get; set; } = string.Empty;

    [Column("ReleaseYear")]
    [NotNull]
    public int ReleaseYear { get; set; }

    [Column("GenreId")]
    public Guid? GenreId { get; set; }

    [ForeignKey(nameof(GenreId))]
    public Genre? Genre { get; set; }

    

    [ForeignKey("StatusId")]
    public int? StatusId { get; set; } = 1;

    public virtual Status? Status { get; set; }

    //public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

}

