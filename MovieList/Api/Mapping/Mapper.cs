using Api.Database.Entities;
using Common.Movies;

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
            ReleaseYear = movie.ReleaseYear
        };
    }
}
