using Banking.Core.Abstractions;
using Banking.Core.Dto;
using Banking.Users.Contracts.Dto;

namespace Banking.Users.Application.Features.Commands.Register.IndividualAccount;

public record RegisterIndividualCommand(
    RegisterDto Register,
    CreateIndividualAccountDto IndividualAccount,
    CreateFileDto File) : ICommand;
