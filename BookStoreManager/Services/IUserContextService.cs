using System.Security.Claims;

namespace BookStoreManager.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        int? GetUserId { get; }
    }
}
