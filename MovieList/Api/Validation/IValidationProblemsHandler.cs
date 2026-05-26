using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Validation;

public interface IValidationProblemsHandler
{
    ValidationProblemDetails Handle(Result<string> result);
    ValidationProblemDetails HandleNull(string messageToSend);
}
