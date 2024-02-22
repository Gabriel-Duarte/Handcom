using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;

namespace Handcom.Web.Services.Interface
{
    public interface IUserProfileService
    {
        Task<Response<UserProfileResponse>> GetUserProfile();
        Task<Response<UserProfileResponse>> UpdateUserProfile(UpdateUserProfileRequest updateUserRequest);
    }
}
