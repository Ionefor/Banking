namespace Banking.UserAccounts.Contracts.Dto;

public record IndividualAccountDto(
    RegisterDto RegisterDto,
    FullNameDto FullName,
    AddressDto Address,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string? Photo);
