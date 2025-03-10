using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Accounts.Contracts.Dto.Queries;

public record FilteringParamsAllAccountsDto(
    Guid? AccountId,
    AddressDto? Address,
    string? Email,
    string? Phone);