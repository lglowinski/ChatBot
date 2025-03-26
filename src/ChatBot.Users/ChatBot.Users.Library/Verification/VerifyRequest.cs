using System.ComponentModel.DataAnnotations;

namespace ChatBot.Users.Library.Verification;

public class VerifyRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string TwoFactorCode { get; set; }
}