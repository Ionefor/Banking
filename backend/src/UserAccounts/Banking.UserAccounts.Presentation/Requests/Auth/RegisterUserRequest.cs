using Banking.SharedKernel;
using Banking.UserAccounts.Application.Command.Auth.Register;
using Banking.UserAccounts.Contracts.Dto;

namespace Banking.UserAccounts.Presentation.Requests.Auth;

public record RegisterUserRequest(
    AccountType AccountType,
    IndividualAccountDto? IndividualAccountDto,
    CorporateAccountDto? CorporateAccountDto)
{
    public RegisterUserCommand ToCommand() =>
        new(AccountType, IndividualAccountDto, CorporateAccountDto);
}