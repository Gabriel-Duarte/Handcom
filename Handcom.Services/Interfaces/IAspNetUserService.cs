using Microsoft.AspNetCore.Http;

namespace Handcom.Services.Interfaces
{
    public interface IAspNetUserService
    {
        string? Name { get; }
        Guid GetUserId();
        string? GetUserEmail();
        bool IsAuthenticated();
        HttpContext? GetHttpContext();
    }
}
