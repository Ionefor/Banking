namespace Banking.Users.Contracts.Dto;

public record RegisterDto(
    string UserName,
    string Password,
    string Email); 