using Web.Domain.Models.ValueObjects;

namespace Web.Domain.Authorization;

public interface IAuthenticatedUserService
{
    public AppUserId AppUserId { get; }
    public Guid? TutorId { get; }
}