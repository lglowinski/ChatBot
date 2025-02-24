namespace ChatBot.Users.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public bool IsTwoFactorEnabled { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    private User() { }

    public User(Guid id, string email, bool isDeleted = false, DateTimeOffset? deletedAt = null)
    {
        Id = id;
        Email = email;
        IsTwoFactorEnabled = false;
        IsDeleted = isDeleted;
        DeletedAt = deletedAt;
    }

    public void EnableTwoFactor()
    {
        IsTwoFactorEnabled = true;
    }

    public void DisableTwoFactor()
    {
        IsTwoFactorEnabled = false;
    }
}