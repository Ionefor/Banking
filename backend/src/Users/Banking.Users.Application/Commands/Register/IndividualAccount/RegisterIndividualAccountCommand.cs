using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Core.Abstractions;
using Banking.Core.Dto;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Application.Commands.Register.IndividualAccount;

public record RegisterIndividualAccountCommand(
    RegisterDto Register,
    CreateIndividualAccountDto IndividualAccount,
    CreateFileDto File) : ICommand;
