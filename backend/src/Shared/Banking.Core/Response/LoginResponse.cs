namespace Banking.Core.Response;

public record LoginResponse(string AccessToken, Guid RefreshToken);