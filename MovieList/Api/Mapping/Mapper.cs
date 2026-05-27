using Api.Database.Entities;
using Common.Movies;
using Common.Genres;
using Common.Statuses;

namespace Api.Mapping;

public static class Mapper
{
    public static MovieBasicDto ToMovieBasicDto(this Movie movie)
    {
        return new MovieBasicDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseYear = movie.ReleaseYear,
            StatusId = movie.StatusId,
            StatusName = movie.Status!.Name
        };
    }

    public static MovieDto ToMovieDto(this Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseYear = movie.ReleaseYear,
            GenreId = movie.GenreId,
            GenreName = movie.Genre != null ? movie.Genre.Name : null,
            StatusId = movie.StatusId,
            StatusName = movie.Status != null ? movie.Status.Name : null,
            AverageRating = movie.Ratings.Any() ? movie.Ratings.Average(rating => rating.Score): null,
            RatingsCount = movie.Ratings.Count
        };
    }

    public static GenreDto ToGenreDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public static StatusDto ToStatusDto(this Status status)
    {
        return new StatusDto
        {
            Id = status.Id,
            Name = status.Name
        };
    }
}
