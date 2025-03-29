using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.ClientAccounts.Domain.Entities;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.BankAccounts.Application.Features.Queries.Cards.GetById;

public class GetCardByIdHandler(IReadDbContext readDbContext) :
    IQueryHandler<Result<CardDto, ErrorList>, GetCardByIdQuery>
{
    public async Task<Result<CardDto, ErrorList>> Handle(
        GetCardByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var cards = await readDbContext.Cards.
            FirstOrDefaultAsync(c => c.Id == query.CardId, cancellationToken);
        
        if (cards is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(Card), nameof(query.CardId),
                    query.CardId)).ToErrorList();
        }

        return cards;
    }
}