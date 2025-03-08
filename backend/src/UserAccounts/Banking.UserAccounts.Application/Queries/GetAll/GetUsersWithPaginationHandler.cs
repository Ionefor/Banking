using Banking.Core.Abstractions;
using Banking.Core.Models;
using Banking.UserAccounts.Domain.Accounts;

namespace Banking.UserAccounts.Application.Queries.GetAll;

// public class GetUsersWithPaginationHandler : 
//     IQueryHandler<PageList<(IReadOnlyList<IndividualAccount>?, IReadOnlyList<CorporateAccount>?)>,
//         GetUsersWithPaginationQuery>
// {
//     public GetUsersWithPaginationHandler()
//     {
//         
//     }
//     public async Task<PageList<
//         (IReadOnlyList<IndividualAccount>?, IReadOnlyList<CorporateAccount>?)>>
//         Handle(GetUsersWithPaginationQuery query, CancellationToken cancellationToken = default)
//     {
//         // var petsQuery = _readDbContext.Pets;
//         //
//         // return await petsQuery.ToPagedList(
//         //     query.PaginationParams.Page,
//         //         query.PaginationParams.PageSize,
//         //         cancellationToken);
//     }
// }