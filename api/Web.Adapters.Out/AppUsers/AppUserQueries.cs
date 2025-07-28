using Microsoft.EntityFrameworkCore;
using Web.Adapters.Out.AppUsers.Get;
using Web.Domain.Authorization;
using Web.Infrastructure.Common.Persistence;
using Web.UseCases.Authorization.Exception;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.Adapters.Out.AppUsers;

public class AppUserQueries(
    IAuthenticatedUserService authenticatedUserService,
    AppDbContext dbContext)
    : IQueryHandler<GetAppUser.Query, GetAppUser.Data>
{
    public async Task<GetAppUser.Data> Handle(GetAppUser.Query query)
    {
        var appUser = await dbContext.AppUser
                          .Include(x => x.AppUserData)
                          .Include(x => x.AppUserAuthorization)
                          .FirstOrDefaultAsync(x => x.AppUserId.Equals(authenticatedUserService.AppUserId))
                      ?? throw new AppUserNotActiveException();


        var appUserData = new GetAppUser.Data(
            appUser.Email.Value,
            appUser.AppUserData.FirstName,
            appUser.AppUserData.LastName,
            appUser.AppUserData.City,
            appUser.AppUserData.PhoneNumberPrefix,
            appUser.AppUserData.PhoneNumber,
            appUser.AppUserAuthorization.IsPasswordRandom);
        
        return appUserData;
    }
}