using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;

namespace Handcom.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDto> GetUserProfile(CancellationToken cancellationToken);
        Task<UserProfileResponseDto> UpdateUserProfile(UpdateUserProfileRequestDto updateUserRequestDto, CancellationToken cancellationToken);
    }
}
