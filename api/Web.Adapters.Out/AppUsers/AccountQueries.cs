using Microsoft.EntityFrameworkCore;
using Web.Adapters.Out.AppUsers.Get;
using Web.Domain.Authorization;
using Web.Infrastructure.Common.Persistence;
using Web.UseCases.Authorization.Exception;

namespace Web.Adapters.Out.AppUsers;

public class AccountQueries(
    IAuthenticatedUserService authenticatedUserService,
    AppDbContext dbContext)
    : IQueryHandler<GetAccount.Query, GetAccount.Data>
{
    public async Task<GetAccount.Data> Handle(GetAccount.Query query)
    {
        var appUser = await dbContext.AppUser
                          .Include(x => x.AppUserData)
                          .Include(x => x.AppUserAuthorization)
                          .FirstOrDefaultAsync(x => x.AppUserId.Equals(authenticatedUserService.AppUserId))
                      ?? throw new AppUserNotActiveException();


        var appUserData = new GetAccount.Data(
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