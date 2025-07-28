using Web.Domain.Models.AppUsers;

namespace Web.Domain.Authorization;

public interface ITokenService
{
    string GetNewJwtTokenFor(AppUser appUser, Guid? tutorId = null);
}