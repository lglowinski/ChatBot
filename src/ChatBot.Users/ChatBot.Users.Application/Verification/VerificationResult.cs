using ChatBot.Users.Domain.Entities;

namespace ChatBot.Users.Application.Verification;

public record VerificationResult(string Token, User User);