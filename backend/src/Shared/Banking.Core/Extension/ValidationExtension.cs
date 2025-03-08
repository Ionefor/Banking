using Banking.SharedKernel.Models.Errors;
using FluentValidation.Results;

namespace Banking.Core.Extension;

public static class ValidationExtension
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(errorMessage)
            select Errors.General.ValueIsInvalid(
                new ErrorParameters.General.ValueIsInvalid(nameof(validationError.PropertyName)));

        return errors.ToList();
    }
}