using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.Users.Contracts;

public interface IUsersContract
{
    Task<HashSet<string>>  GetUserPermissionsCodes(Guid userId);
    
    Task<UnitResult<ErrorList>>  DeleteUser(Guid userId);
}