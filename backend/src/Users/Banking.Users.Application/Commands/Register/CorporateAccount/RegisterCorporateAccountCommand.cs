using Banking.Core.Abstractions;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Application.Commands.Register.CorporateAccount;

public record RegisterCorporateAccountCommand( 
    RegisterDto Register,
    CreateCorporateAccountDto CorporateAccount) : ICommand;