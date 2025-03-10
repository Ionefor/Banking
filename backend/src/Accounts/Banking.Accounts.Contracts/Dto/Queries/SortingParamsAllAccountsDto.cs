namespace Banking.Accounts.Contracts.Dto.Queries;

public record SortingParamsAllAccountsDto(
    bool? AccountsId,
    bool? Address);