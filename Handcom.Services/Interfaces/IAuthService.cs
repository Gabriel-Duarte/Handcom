using Handcom.Domain.Dto.Extensions;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;

namespace Handcom.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterUserResponseDto> CreateUserAsync(RegisterUserRequestDto registerUser, CancellationToken cancellationToken);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);
        Task<TokenDto?> GetRefreshTokenAsync(TokenDto refreshToken, CancellationToken cancellationToken);
    }
}
