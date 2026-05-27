using Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Database;

public class SanContext : DbContext
{
    public SanContext(DbContextOptions<SanContext> options) : base(options) { }
    public required virtual DbSet<Status> Statuses { get; set; }
    public required virtual DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasIndex(movie => movie.Title).IsUnique();
            entity.Property(movie => movie.Director) .HasMaxLength(150).IsRequired();
            entity.Property(movie => movie.ReleaseYear).IsRequired();
            entity.HasOne(movie => movie.Status);
            // entity.HasMany(movie => movie.Notes);
        });

        modelBuilder.Entity<Movie>(entity =>
{

        // modelBuilder.Entity<Course>(entity =>
        // {
        //     entity.HasMany(course => course.Movies).WithMany(movie => movie.Courses);
        // });

        // modelBuilder.Entity<Note>().Property(p => p.Value).HasPrecision(17, 2);

        var initialStatuses = CreateStatuses();
        modelBuilder.Entity<Status>().HasData(initialStatuses);
    });
    
    }

    private static Status[] CreateStatuses()
    {
        return new[]
        {
            new Status { Id = 1, Name = "Nieobejrzany" },
            new Status { Id = 2, Name = "Obejrzany" }
        };
    }
}
