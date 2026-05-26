using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Validation
{
    public class ValidationProblemsHandler : IValidationProblemsHandler
    {
        public ValidationProblemDetails Handle(Result<string> result)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Błąd zapisu: ",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            var validationErrors = result.Errors.OfType<ValidationError>();
            var groupedErrors = validationErrors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.Message).ToArray()
                );

            var genericErrors = result.Errors.Where(e => e is not ValidationError).ToList();
            if (genericErrors.Any())
                groupedErrors.Add("General", genericErrors.Select(e => e.Message).ToArray());

            problemDetails.Errors = groupedErrors;
            return problemDetails;
        }

        public ValidationProblemDetails HandleNull(string messageToSend)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Błąd pobierania: ",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            var validationErrors = new List<ValidationError>
        {
            new ValidationError("Id", messageToSend)
        };

            var groupedErrors = validationErrors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.Message).ToArray()
                );

            problemDetails.Errors = groupedErrors;
            return problemDetails;
        }
    }
}
