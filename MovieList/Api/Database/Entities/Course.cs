using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Api.Database.Entities
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Column("Name")]
        [NotNull]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Student>? Students { get; set; }
    }
}
