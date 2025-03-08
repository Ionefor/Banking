using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Domain.Accounts;
using Banking.UserAccounts.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Banking.UserAccounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<Role> Roles { get; init; } = [];
    public FullName FullName { get; init; } = null!;
   
    public static Result<User, Error> CreateIndividualAccount(
        FullName fullName, string userName, string email, Role role)
    {
        if (role.Name != IndividualAccount.Individual)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Individual"));
        }
        
        return new User
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
    
    public static Result<User, Error> CreateCorporateAccount(
        FullName fullName, string userName, string email, Role role)
    {
        if (role.Name != CorporateAccount.Corporate)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Corporate"));
        }
        
        return new User
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
    
    public static Result<User, Error> CreateAdmin(string userName, string email, Role role)
    {
        if (role.Name != AdminAccount.Admin)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Admin"));
        }
        
        return new User
        {
            FullName = FullName.Create(userName, userName, userName).Value,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}