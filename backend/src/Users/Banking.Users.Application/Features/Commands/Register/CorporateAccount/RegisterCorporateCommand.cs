using Banking.Core.Abstractions;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Application.Features.Commands.Register.CorporateAccount;

public record RegisterCorporateCommand( 
    RegisterDto Register,
    CreateCorporateAccountDto CorporateAccount) : ICommand;