using Handcom.Domain.Dto.Extensions;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterUserResponseDto> CreateUserAsync(RegisterUserRequestDto registerUser, CancellationToken cancellationToken);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);
        Task<TokenDto?> GetRefreshTokenAsync(TokenDto refreshToken, CancellationToken cancellationToken);
    }
}
