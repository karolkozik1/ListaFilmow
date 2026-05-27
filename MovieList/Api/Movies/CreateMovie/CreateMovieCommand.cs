using Common.Movies;
using FluentResults;
using MediatR;

namespace Api.Movies.CreateMovie;

public record CreateMovieCommand(CreateMovieRequest Request) : IRequest<Result<string>>;