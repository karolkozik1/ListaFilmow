using Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Database;

public class SanContext : DbContext
{
    public SanContext(DbContextOptions<SanContext> options) : base(options) { }
    public required virtual DbSet<Status> Statuses { get; set; }
    public required virtual DbSet<Student> Students { get; set; }
    public required virtual DbSet<Course> Courses { get; set; }
    public required virtual DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(student => student.AlbumNumber).IsUnique();
            entity.HasOne(student => student.Status);
            entity.HasMany(student => student.Notes);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(course => course.Students).WithMany(student => student.Courses);
        });

        modelBuilder.Entity<Note>().Property(p => p.Value).HasPrecision(17, 2);

        var initialStatuses = CreateStatuses();
        modelBuilder.Entity<Status>().HasData(initialStatuses);
    }

    private static Status[] CreateStatuses()
    {
        return new[]
        {
            new Status { Id = 1, Name = "Aktywny" },
            new Status { Id = 2, Name = "Skreślony" }
        };
    }
}
