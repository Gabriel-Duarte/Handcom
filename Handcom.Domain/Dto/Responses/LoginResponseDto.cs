using Handcom.Domain.Dto.Extensions;

namespace Handcom.Domain.Dto.Responses
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; } = null!;
    }
}
