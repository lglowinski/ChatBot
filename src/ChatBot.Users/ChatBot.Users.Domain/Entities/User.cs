namespace ChatBot.Users.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public bool IsTwoFactorEnabled { get; private set; }

    private User() { }

    public User(Guid id, string email)
    {
        Id = id;
        Email = email;
        IsTwoFactorEnabled = false;
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