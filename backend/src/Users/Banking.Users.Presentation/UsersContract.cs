using Banking.SharedKernel.Models.Errors;
using Banking.Users.Application.Abstractions;
using Banking.Users.Contracts;
using Banking.Users.Domain;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Banking.Users.Presentation;

public class UsersContract : IUsersContract
{
    private readonly UserManager<User> _userManager;
    private readonly IPermissionManager _permissionManager;
    public UsersContract(
        UserManager<User> userManager,
        IPermissionManager permissionManager)
    {
        _userManager = userManager;
        _permissionManager = permissionManager;
    }
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        return _permissionManager.GetUserPermissions(userId);
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