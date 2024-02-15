using Handcom.Api.Controllers.Base;
using Handcom.Domain.Dto.Extensions;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Services.Interfaces;
using Handcom.Services.Responses;
using Handcom.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Handcom.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(INotifierService notifierService, IAuthService authService) : base(notifierService) =>
            _authService = authService;


        [HttpPost("Register")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterUserResponseDto>> CreateUser(RegisterUserRequestDto registerUserRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _authService.CreateUserAsync(registerUserRequestDto, CancellationToken.None).ConfigureAwait(false));
        }


        [HttpPost("Login")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _authService.LoginAsync(loginRequestDto, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPost("Refresh-Token")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenDto>> RefreshToken(TokenDto refreshToken)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _authService.GetRefreshTokenAsync(refreshToken, CancellationToken.None).ConfigureAwait(false));
        }
    }
}
