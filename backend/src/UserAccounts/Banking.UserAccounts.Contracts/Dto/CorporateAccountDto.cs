namespace Banking.UserAccounts.Contracts.Dto;

public record CorporateAccountDto(
    string CompanyName,
    RegisterDto RegisterDto,
    FullNameDto FullName,
    AddressDto Address,
    string ContactPhone,
    string TaxId); 
