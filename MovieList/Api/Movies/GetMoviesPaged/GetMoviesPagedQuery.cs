using Common.CommonData;
using Common.Movies;
using MediatR;

namespace Api.Movies.GetMoviesPaged;

public record GetMoviesPagedQuery(GetMoviesPagedRequest Request) : IRequest<PagedList<MovieDto>>;