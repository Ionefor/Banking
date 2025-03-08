namespace Banking.UserAccounts.Contracts.Dto.Commands;

public record CorporateAccountDto(
    string CompanyName,
    RegisterDto RegisterDto,
    FullNameDto FullName,
    AddressDto Address,
    string ContactPhone,
    string TaxId); 
