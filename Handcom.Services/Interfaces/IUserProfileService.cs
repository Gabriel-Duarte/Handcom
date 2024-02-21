using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDto> GetUserProfile(CancellationToken cancellationToken);
        Task<UserProfileResponseDto> UpdateUserProfile(UpdateUserProfileRequestDto updateUserRequestDto, CancellationToken cancellationToken);
    }
}
