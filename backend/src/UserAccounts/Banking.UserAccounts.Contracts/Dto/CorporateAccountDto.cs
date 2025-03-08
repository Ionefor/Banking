namespace Banking.UserAccounts.Contracts.Dto;

public record CorporateAccountDto(
    string CompanyName,
    RegisterDto RegisterDto,
    string ContactEmail,
    FullNameDto FullName,
    AddressDto Address,
    string ContactPhone,
    string TaxId); 
