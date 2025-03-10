using Banking.SharedKernel.Models.Errors;
using Banking.Users.Contracts;
using Banking.Users.Domain;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Banking.Users.Presentation;

public class UsersContract : IUsersContract
{
    private readonly UserManager<User> _userManager;

    public UsersContract(
        UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UnitResult<ErrorList>> DeleteUser(Guid userId)
    {
        var user = await _userManager.
            FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(User), nameof(userId), userId)).ToErrorList();
        }
        
        var deletingUser = await _userManager.DeleteAsync(user);

        if (!deletingUser.Succeeded)
        {
            var errors = deletingUser.Errors.Select(e =>
                Errors.General.Failed(new ErrorParameters.Failed(e.Description)));
              
            return new ErrorList(errors);
        }
        
        return UnitResult.Success<ErrorList>();
    }
}