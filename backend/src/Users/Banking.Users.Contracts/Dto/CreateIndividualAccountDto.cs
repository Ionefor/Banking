using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Users.Contracts.Dto;

public record CreateIndividualAccountDto(
    FullNameDto FullName,
    AddressDto Address,
    string PhoneNumber,
    DateOnly DateOfBirth);
