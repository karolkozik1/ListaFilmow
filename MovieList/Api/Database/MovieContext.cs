using Api.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    public required virtual DbSet<Movie> Movies { get; set; }
    public required virtual DbSet<Status> Statuses { get; set; }
    public required virtual DbSet<Genre> Genres { get; set; }
    public required virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(movie => movie.Id);

            entity.Property(movie => movie.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(movie => movie.Director)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(movie => movie.ReleaseYear)
                .IsRequired();

            entity.Property(movie => movie.GenreId)
                .IsRequired(false);

            entity.HasOne(movie => movie.Genre)
                .WithMany(genre => genre.Movies)
                .HasForeignKey(movie => movie.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(movie => movie.StatusId)
                .IsRequired(false);

            entity.HasOne(movie => movie.Status)
                .WithMany(status => status.Movies)
                .HasForeignKey(movie => movie.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(genre => genre.Id);

            entity.Property(genre => genre.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(genre => genre.Name)
                .IsUnique();

            entity.HasData(
                new Genre
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Akcja"
                },
                new Genre
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Name = "Dramat"
                },
                new Genre
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Name = "Komedia"
                },
                new Genre
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Name = "Science fiction"
                },
                new Genre
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Name = "Horror"
                });
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(rating => rating.Id);

            entity.Property(rating => rating.Score)
                .IsRequired();

            entity.Property(rating => rating.Comment)
                .HasMaxLength(1000);

            entity.HasOne(rating => rating.Movie)
                .WithMany(movie => movie.Ratings)
                .HasForeignKey(rating => rating.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable(table =>
            {
                table.HasCheckConstraint("CK_Ratings_Score", "[Score] >= 1 AND [Score] <= 10");
            });
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(status => status.Id);

            entity.Property(status => status.Name)
                .HasMaxLength(20)
                .IsRequired();

            entity.HasData(CreateStatuses());
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