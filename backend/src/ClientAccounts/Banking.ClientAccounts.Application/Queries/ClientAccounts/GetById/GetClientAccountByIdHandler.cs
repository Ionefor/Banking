using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.ClientAccounts.Domain.Aggregate;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.BankAccounts.Application.Queries.ClientAccounts.GetById;

public class GetClientAccountByIdHandler : 
    IQueryHandler<Result<ClientAccountDto, ErrorList>, GetClientAccountByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetClientAccountByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<ClientAccountDto, ErrorList>> Handle(
        GetClientAccountByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var clientAccount = await _readDbContext.ClientAccounts.
            FirstOrDefaultAsync(c => c.Id == query.ClientAccountId, cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound(
                    nameof(ClientAccount), nameof(query.ClientAccountId),
                        query.ClientAccountId)).ToErrorList();
        }

        return clientAccount;
    }
}
