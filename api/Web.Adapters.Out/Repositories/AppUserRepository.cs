using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Models.AppUsers;
using Web.Domain.Models.ValueObjects;
using Web.Domain.Repositories;
using Web.Infrastructure.Common.Persistence;

namespace Web.Adapters.Out.Repositories;

public class AppUserRepository(
    AppDbContext dbContext)
    : IAppUserRepository
{
    public async Task<AppUser?> GetByIdAsync(
        AppUserId id,
        params Expression<Func<AppUser, object>>[] includes)
    {
        var query = includes.Length != 0
            ? includes
                .Aggregate<Expression<Func<AppUser, object>>?, IQueryable<AppUser>>(
                    dbContext.AppUser,
                    (current, include) =>
                        current.Include(include))
            : dbContext.AppUser;

        return await query.FirstOrDefaultAsync(u => u.AppUserId == id);
    }

    public async Task<AppUser?> GetByEmailAsync(string email, params Expression<Func<AppUser, object>>[] includes)
    {
        var query = includes.Length != 0
            ? includes
                .Aggregate<Expression<Func<AppUser, object>>?, IQueryable<AppUser>>(
                    dbContext.AppUser,
                    (current, include) =>
                        current.Include(include))
            : dbContext.AppUser;

        return await query.FirstOrDefaultAsync(u => u.Email == new Email(email));
    }

    public async Task<bool> EmailAlreadyExists(string email)
    {
        return await dbContext.AppUser.AnyAsync(x => x.Email == new Email(email));
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync(
        params Expression<Func<AppUser, object>>[] includes)
    {
        var query = includes.Length != 0
            ? includes
                .Aggregate<Expression<Func<AppUser, object>>?, IQueryable<AppUser>>(
                    dbContext.AppUser,
                    (current, include) =>
                        current.Include(include))
            : dbContext.AppUser;

        return await query.ToListAsync();
    }

    public async Task AddAsync(AppUser user)
    {
        await dbContext.AppUser.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppUser user)
    {
        dbContext.AppUser.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AppUserId id)
    {
        var user = await dbContext.AppUser.FindAsync(id);
        if (user is not null)
        {
            dbContext.AppUser.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}