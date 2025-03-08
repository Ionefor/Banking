namespace Banking.UserAccounts.Contracts.Dto;

public record IndividualAccountDto(
    RegisterDto RegisterDto,
    FullNameDto FullName,
    string ContactEmail,
    AddressDto Address,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string? Photo);
