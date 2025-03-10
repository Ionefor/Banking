namespace Banking.Accounts.Contracts.Dto.Queries;

public record SortingParamsIndividualAccountsDto(
    bool? Address,
    bool? DateOfBirth,
    bool? PhoneNumber);
