using Common.CommonData;
using Common.Movies;
using MediatR;

namespace Api.Movies.GetMovieById;

public record GetMovieByIdQuery(string Id) : IRequest<MovieDto?>;

