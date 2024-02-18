using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
