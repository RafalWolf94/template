namespace Web.Domain.Authorization;

public interface IPasswordService
{
    string HashPassword(string password);
    string GenerateRandomPassword();
    bool VerifyPassword(string password, string hashedPassword);
}