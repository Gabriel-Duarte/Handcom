using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;

namespace Handcom.Web.Services.Interface
{
    public interface IAuthService
    {
        Task<Response<LoginResponse>> Login(LoginRequest loginModel);
        Task<Response<RegisterUserResponse>> Register(RegisterUserRequest registerUser);
        void Logout();
    }
}
