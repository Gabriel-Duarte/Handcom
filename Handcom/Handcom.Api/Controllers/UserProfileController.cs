using Handcom.Api.Controllers.Base;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Handcom.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserProfileController : MainController
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(INotifierService notifierService, IUserProfileService userProfileService) : base(notifierService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Posts>> Get() =>
           CustomResponse(await _userProfileService.GetUserProfile(CancellationToken.None).ConfigureAwait(false));


        [HttpPut]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Posts>> UpdateUserProfile(UpdateUserProfileRequestDto updateUserProfileRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _userProfileService.UpdateUserProfile(updateUserProfileRequestDto, CancellationToken.None).ConfigureAwait(false));
        }
    }
}

