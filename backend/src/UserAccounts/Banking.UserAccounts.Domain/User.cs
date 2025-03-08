using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Domain.Accounts;
using Banking.UserAccounts.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Banking.UserAccounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<Role> Roles { get; init; } = [];
    
    public static Result<User, Error> CreateIndividualAccount(
        string userName, string email, Role role)
    {
        if (role.Name != IndividualAccount.Individual)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Individual"));
        }
        
        return new User
        {
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
    
    public static Result<User, Error> CreateCorporateAccount(
       string userName, string email, Role role)
    {
        if (role.Name != CorporateAccount.Corporate)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Corporate"));
        }
        
        return new User
        {
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
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}