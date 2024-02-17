using Handcom.Web.Model.Extensions;

namespace Handcom.Web.Model.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserToken UserToken { get; set; } = null!;
    }
}
