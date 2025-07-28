using JetBrains.Annotations;
using Web.Domain.Authorization;
using Web.Domain.Repositories;
using Web.UseCases.Account.Exception;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.UseCases.Account.Delete;


[PublicAPI]
public sealed class AccountDeleteCommandHandler(
    IAuthenticatedUserService authenticatedUserService,
    IAppUserRepository appUserRepository
) : ICommandHandler<AccountDelete.Command>
{
    public async Task Handle(AccountDelete.Command command)
    {
        
        var appUser = await appUserRepository
                          .GetByIdAsync(authenticatedUserService.AppUserId)
                      ?? throw new AppUserNotFoundException();

        await appUserRepository.DeleteAsync(appUser.AppUserId);
    }
}