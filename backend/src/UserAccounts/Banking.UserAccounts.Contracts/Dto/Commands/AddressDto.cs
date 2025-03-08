namespace Banking.UserAccounts.Contracts.Dto.Commands;

public record AddressDto(
    string Country,
    string City,
    string Street,
    string HouseNumber);