using Common.CommonData;
using Common.Movies;
using MediatR;

namespace Api.Movies.GetMovieById;

public record GetMovieByIdQuery(Guid Id) : IRequest<MovieDto?>;

