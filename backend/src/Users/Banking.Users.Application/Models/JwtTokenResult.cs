namespace Banking.Users.Application.Models;

public record JwtTokenResult(string AccessToken, Guid Jti);