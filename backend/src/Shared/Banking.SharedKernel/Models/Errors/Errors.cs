namespace Banking.SharedKernel.Models.Errors;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(ErrorParameters.ValueIsInvalid? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsInvalid,
                ErrorConstants.GeneralMessage.ValueIsInvalid,
                ErrorType.Validation,
                parameters?.SubjectName is null ? null : $"{parameters.SubjectName} is invalid",
                parameters?.SubjectName);
        }

        public static Error NotFound(ErrorParameters.NotFound? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.NotFound,
                ErrorConstants.GeneralMessage.NotFound,
                ErrorType.NotFound,
                parameters is null ? null
                    : $"{parameters.ObjectName} record not found with {parameters.SubjectType}:" +
                      $" {parameters.SubjectValue}");
        }

        public static Error ValueIsRequired(ErrorParameters.ValueIsRequired? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsRequired,
                ErrorConstants.GeneralMessage.ValueIsRequired,
                ErrorType.Validation,
                parameters is null ? null
                    : $"{parameters.SubjectName} cannot be null or empty");
        }

        public static Error InternalServer(ErrorParameters.InternalServer? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.InternalServer,
                ErrorConstants.GeneralMessage.InternalServer,
                ErrorType.Failure,
                parameters?.Message);
        }

        public static Error Failed(ErrorParameters.Failed? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.Failed,
                ErrorConstants.GeneralMessage.Failed,
                ErrorType.Failure,
                parameters?.Message);
        }
    }

    public static class Extra
    {
        public static Error RoleIsInvalid(ErrorParameters.RoleIsInvalid? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsInvalid,
                ErrorConstants.ExtraMessage.InvalidRole,
                ErrorType.Conflict,
                parameters?.Message);
        }

        public static Error InvalidDeleteOperation(ErrorParameters.InvalidDeleteOperation? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.DeleteIsInvalid,
                ErrorConstants.GeneralMessage.DeleteIsInvalid,
                ErrorType.Conflict,
                parameters is null ? null
                    : $"{parameters.SubjectName} with {parameters.SubjectValue} cannot be deleted");
        }

        public static Error TokenIsInvalid()
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsInvalid,
                ErrorConstants.ExtraMessage.InvalidToken,
                ErrorType.Validation);
        }

        public static Error TokenIsExpired()
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsInvalid,
                ErrorConstants.ExtraMessage.ExpiredToken,
                ErrorType.Failure);
        }

        public static Error CredentialsIsInvalid()
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueIsInvalid,
                ErrorConstants.ExtraMessage.InvalidCredentials,
                ErrorType.Validation);
        }

        public static Error AlreadyExists(ErrorParameters.ValueAlreadyExists? parameters = null)
        {
            return ErrorFactory.Create(
                ErrorConstants.Code.ValueAlreadyExists,
                ErrorConstants.ExtraMessage.ValueAlreadyExists,
                ErrorType.Conflict,
                parameters?.Message);
        }
    }
}