using Handcom.Domain.Extensions;
using Handcom.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Handcom.Services.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUserService(IHttpContextAccessor accessor) =>
            _accessor = accessor;

        public string? Name => _accessor.HttpContext?.User.Identity?.Name;

        public Guid GetUserId() =>
            IsAuthenticated() ? Guid.Parse(_accessor.HttpContext?.User.GetUserId() ?? string.Empty) : Guid.Empty;

        public string? GetUserEmail() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() : "";

        public bool IsAuthenticated() =>
            _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;


        public HttpContext? GetHttpContext() =>
            _accessor.HttpContext;
    }
}
