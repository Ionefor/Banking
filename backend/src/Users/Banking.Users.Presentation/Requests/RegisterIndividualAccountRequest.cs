using Banking.Core.Dto;
using Banking.Users.Application.Commands.Register.IndividualAccount;
using Banking.Users.Contracts.Dto;
using Microsoft.AspNetCore.Http;

namespace Banking.Users.Presentation.Requests;

public record RegisterIndividualAccountRequest(
    RegisterDto Register,
    CreateIndividualAccountDto IndividualAccount,
    IFormFile File)
{
    public RegisterIndividualAccountCommand ToCommand(CreateFileDto file) 
        => new(Register, IndividualAccount, file);
}