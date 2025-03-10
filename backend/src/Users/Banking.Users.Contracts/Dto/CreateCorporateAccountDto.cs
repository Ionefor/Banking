using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Users.Contracts.Dto;

public record CreateCorporateAccountDto(
    string CompanyName,
    AddressDto Address,
    string ContactPhone,
    string TaxId); 
