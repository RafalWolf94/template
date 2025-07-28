using JetBrains.Annotations;
using Web.Domain.Authorization;
using Web.Domain.Repositories;
using Web.UseCases.Authorization.Exception;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.UseCases.Authorization.Authorize;

[PublicAPI]
public sealed class AuthorizeCommandHandler(
    IAppUserRepository appUserRepository,
    IPasswordService passwordService,
    ITokenService tokenService)
    : ICommandHandler<Authorize.Command, Authorize.Data>
{
    public async Task<Authorize.Data> Handle(Authorize.Command command)
    {
        var appUser = await appUserRepository
                          .GetByEmailAsync(command.Email,
                              user => user.AppUserAuthorization,
                              user => user.AppUserData)
                      ?? throw new InvalidCredentialsException();

        if (!appUser.IsActive)
            throw new AppUserNotActiveException();

        if (appUser.AppUserAuthorization is null)
            throw new InvalidCredentialsException();

        var verifyPassword = passwordService
            .VerifyPassword(
                command.Password,
                appUser.AppUserAuthorization.Password
            );

        if (!verifyPassword)
            throw new InvalidCredentialsException();

        var token = tokenService.GetNewJwtTokenFor(appUser);
        return new Authorize.Data(token);
    }
}