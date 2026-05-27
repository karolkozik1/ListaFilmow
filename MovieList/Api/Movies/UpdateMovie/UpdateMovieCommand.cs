using Common.Movies;
using FluentResults;
using MediatR;

namespace Api.Movies.UpdateMovie;

public record UpdateMovieCommand(Guid Id, UpdateMovieRequest Request) : IRequest<Result<string>>;