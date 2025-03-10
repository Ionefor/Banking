namespace Banking.SharedKernel.Models.Errors;

public record Error
{
    public const string Separator = "||";
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public string? StackTrace { get; init; }

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }
    
    internal static Error CreateError(
        string code, string message, ErrorType type, string? invalidField = null)
    {
        return new Error(code, message, type, invalidField)
        {
            StackTrace = Environment.StackTrace
        };
    }
    
    public string Serialize()
    {
        return string.Join(Separator, Code, Message, Type);
    }

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(Separator);

        if (parts.Length < 3) throw new ArgumentException("Invalid serialized format");

        if (!Enum.TryParse<ErrorType>(parts[2], out var type))
            throw new ArgumentException("Invalid serialized format");

        return new Error(parts[0], parts[1], type);
    }

    public ErrorList ToErrorList()
    {
        return new ErrorList([this]);
    }
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}