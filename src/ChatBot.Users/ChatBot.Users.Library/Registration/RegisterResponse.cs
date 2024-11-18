namespace ChatBot.Users.Library.Registration;

public record RegisterResponse(Guid UserId, string Email, string SecretKey, string QrCodeUri);