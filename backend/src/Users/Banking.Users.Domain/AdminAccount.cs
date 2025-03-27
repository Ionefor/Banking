namespace Banking.Users.Domain;

public class AdminAccount
{
    public static string Admin = nameof(Admin);
    private AdminAccount() {}
    
    public static AdminAccount Create(User user)
    {
        return new AdminAccount
        {
            Id = Guid.NewGuid(),
            User = user,
        };
    }
    public Guid Id { get; init; }
    public User User { get; init; }
    public Guid UserId { get; init; }
}