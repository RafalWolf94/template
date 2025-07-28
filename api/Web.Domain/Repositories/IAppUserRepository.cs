using System.Linq.Expressions;
using Web.Domain.Models.AppUsers;
using Web.Domain.Models.ValueObjects;

namespace Web.Domain.Repositories;

public interface IAppUserRepository
{
    Task<AppUser?> GetByIdAsync(
        AppUserId id,
        params Expression<Func<AppUser, object>>[] includes);

    Task<AppUser?> GetByEmailAsync(
        string email,
        params Expression<Func<AppUser, object>>[] includes);

    Task<IEnumerable<AppUser>> GetAllAsync(
        params Expression<Func<AppUser, object>>[] includes);

    Task AddAsync(AppUser user);
    Task UpdateAsync(AppUser user);
    Task DeleteAsync(AppUserId id);
    Task<bool> EmailAlreadyExists(string commandEmail);
}