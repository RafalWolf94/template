using Web.Domain.Authorization;
using Web.Domain.Repositories;
using Web.UseCases.Account.Exception;

namespace Web.UseCases.Account.Edit;

[PublicAPI]
public sealed class AccountEditCommandHandler(
    IAppUserRepository appUserRepository,
    IAuthenticatedUserService authenticatedUserService
)
    : ICommandHandler<AccountEdit.Command>
{
    public async Task Handle(AccountEdit.Command command)
    {
        var appUser = await appUserRepository
                          .GetByIdAsync(
                              authenticatedUserService.AppUserId,
                              user => user.AppUserData)
                      ?? throw new AppUserNotFoundException();

        appUser.AppUserData.Update(command.FirstName, command.LastName, command.City, command.PhoneNumberPrefix, command.PhoneNumber);

        await appUserRepository.UpdateAsync(appUser);
    }
}