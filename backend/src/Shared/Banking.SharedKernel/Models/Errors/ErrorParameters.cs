namespace Banking.SharedKernel.Models.Errors;
public static class ErrorParameters
{
    public record NotFound(string ObjectName, string SubjectType, object SubjectValue);
    public record ValueIsRequired(string SubjectName);
    public record ValueIsInvalid(string SubjectName);
    public record InternalServer(string Message);
    public record Failed(string Message);
    public record PositionIsInvalid(int Position);
    public record InvalidDeleteOperation(string SubjectName, object SubjectValue);
    public record ValueAlreadyExists(string Message);
    public record RoleIsInvalid(string Message);
}
