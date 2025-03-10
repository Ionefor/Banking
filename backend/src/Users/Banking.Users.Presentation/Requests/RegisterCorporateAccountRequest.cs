using Banking.Users.Application.Commands.Register.CorporateAccount;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Presentation.Requests;

public record RegisterCorporateAccountRequest(
    RegisterDto Register,
    CreateCorporateAccountDto CorporateAccount)
{
    public RegisterCorporateAccountCommand ToCommand() 
        => new(Register, CorporateAccount);
}