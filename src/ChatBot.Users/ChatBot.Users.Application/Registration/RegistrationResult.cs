namespace ChatBot.Users.Application.Registration;

public record RegistrationResult(Guid UserId, string Email, string SecretKey, string QrCodeUri);