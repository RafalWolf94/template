using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Domain.Authorization;
using Web.Domain.Models.AppUsers;

namespace Web.Infrastructure.Services.Auth;

public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GetNewJwtTokenFor(AppUser user, Guid? tutorId = null)
    {
        var jwtClaims = CreateJwtClaims(user,tutorId);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
     
        var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, jwtClaims,
            expires: DateTime.Now.AddHours(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static IEnumerable<Claim> CreateJwtClaims(AppUser user, Guid? tutorId = null)
    {
        var claims = new List<Claim>
        {
            new("uid", user.AppUserId.Value.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.GivenName, user.AppUserData.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.AppUserData.LastName)

        };

        if (!tutorId.HasValue)
            return claims;
        
        claims.Add(new Claim("tutorId", tutorId.Value.ToString()));

        return claims;
    }
}