namespace Banking.UserAccounts.Contracts.Response;

public record LoginResponse(string AccessToken, Guid RefreshToken);