using FluentResults;

namespace Api.Validation;

public class ValidationError : Error
{
    public string PropertyName { get; }
    public ValidationError(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}

