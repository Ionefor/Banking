namespace Banking.UserAccounts.Contracts.Dto.Commands;

public record RegisterDto(
    string UserName,
    string Password,
    string Email); 