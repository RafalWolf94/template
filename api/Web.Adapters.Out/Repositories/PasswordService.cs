using System.Text;
using Web.Domain.Authorization;
using static BCrypt.Net.BCrypt;

namespace Web.Adapters.Out.Repositories;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        var salt = GenerateSalt(12);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }

    public string GenerateRandomPassword()
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+";
        var password = new StringBuilder();
        var random = new Random();

        for (var i = 0; i < 16; i++)
        {
            var index = random.Next(validChars.Length);
            password.Append(validChars[index]);
        }

        return password.ToString();
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return Verify(password, hashedPassword);
    }
}