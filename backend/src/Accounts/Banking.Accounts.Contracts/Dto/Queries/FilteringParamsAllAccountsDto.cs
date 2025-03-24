using Banking.Accounts.Contracts.Dto.Commands;
using Banking.SharedKernel;

namespace Banking.Accounts.Contracts.Dto.Queries;

public record FilteringParamsAllAccountsDto(
    Guid? AccountId,
    AddressDto? Address,
    AccountType? AccountType,
    string? Email,
    string? Phone);