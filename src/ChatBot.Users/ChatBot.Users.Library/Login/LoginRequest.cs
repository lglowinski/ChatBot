using System.ComponentModel.DataAnnotations;

namespace ChatBot.Users.Library.Login;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}