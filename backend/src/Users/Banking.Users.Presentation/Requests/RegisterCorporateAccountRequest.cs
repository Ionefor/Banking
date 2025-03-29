using Banking.Users.Application.Features.Commands.Register.CorporateAccount;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Presentation.Requests;

public record RegisterCorporateAccountRequest(
    RegisterDto Register,
    CreateCorporateAccountDto CorporateAccount)
{
    public RegisterCorporateCommand ToCommand() 
        => new(Register, CorporateAccount);
}