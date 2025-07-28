using Web.Domain.Authorization;
using Web.Domain.Models.AppUsers;
using Web.Domain.Models.ValueObjects;
using Web.Domain.Repositories;
using Web.UseCases.Account.Exception;

namespace Web.UseCases.Account.Create;

[PublicAPI]
public sealed class AccountCreateCommandHandler(
    IPasswordService passwordService,
    IAppUserRepository appUserRepository)
    : ICommandHandler<AccountCreate.Command>
{
    public async Task Handle(AccountCreate.Command command)
    {
        var alreadyExists = await appUserRepository
            .EmailAlreadyExists(command.Email);

        if (alreadyExists)
            throw new EmailAlreadyTakenException();


        await CreateAppUser(command.Email, command.Password, command.AcceptTerms);
    }

    private async Task CreateAppUser(string email, string password, bool acceptTerms)
    {
        var isPasswordRandom = string.IsNullOrWhiteSpace(password);
        var currentPassword = isPasswordRandom
            ? passwordService.GenerateRandomPassword()
            : password;
        
        var hashedPassword = passwordService.HashPassword(currentPassword);
        
        var appUserData = AppUserData.Create(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        var appUserAuthorization = AppUserAuthorization.Create(hashedPassword, isPasswordRandom);
        var appUser = AppUser.Create(new Email(email), acceptTerms, appUserAuthorization, appUserData);
        await appUserRepository.AddAsync(appUser);
    }
}