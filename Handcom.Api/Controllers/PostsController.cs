using Handcom.Api.Controllers.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
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
    public class PostsController : MainController
    {
        private readonly IPostsService _postsService;

        public PostsController(INotifierService notifierService, IPostsService postsService) : base(notifierService)
        {
            _postsService = postsService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Posts>>> Get([FromQuery] PostsPage PostsPage) =>
           CustomResponse(await _postsService.GetAsync(PostsPage, CancellationToken.None).ConfigureAwait(false));


        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Posts>> Create(PostsCreateRequestDto postsRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _postsService.CreateAsync(postsRequestDto, CancellationToken.None).ConfigureAwait(false));
        }
    }
}
