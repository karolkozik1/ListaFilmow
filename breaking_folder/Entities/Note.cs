using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Api.Database.Entities
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Value")]
        [NotNull]
        [StringLength(200)]
        public decimal Value { get; set; }

        [Column("CreatedAt")]
        [NotNull]
        public DateOnly CreatedAt { get; set; }
    }
}
