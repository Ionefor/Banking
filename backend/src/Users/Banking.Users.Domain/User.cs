using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Banking.Users.Domain;

public class User : IdentityUser<Guid>
{
    public List<Role> Roles { get; init; } = [];
    
    public static Result<User, Error> CreateIndividual(
        string userName, string email, Role role)
    {
        if (role.Name != Constants.Accounts.Individual)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.RoleIsInvalid("Role must be an Individual"));
        }
        
        return new User
        {
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
    
    public static Result<User, Error> CreateCorporate(
        string userName, string email, Role role)
    {
        if (role.Name != Constants.Accounts.Corporate)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.RoleIsInvalid("Role must be an Corporate"));
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
            return Errors.Extra.RoleIsInvalid(
                new ErrorParameters.RoleIsInvalid("Role must be an Admin"));
        }
        
        return new User
        {
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}