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
    [Authorize]
    public class CommentsController : MainController
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(INotifierService notifierService, ICommentsService commentsService) : base(notifierService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Comments>>> Get([FromQuery] CommentsPage commentsPage) =>
           CustomResponse(await _commentsService.GetAsync(commentsPage, CancellationToken.None).ConfigureAwait(false));


        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Comments>> Create(CommentsCreateRequestDto commentsRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _commentsService.CreateAsync(commentsRequestDto, CancellationToken.None).ConfigureAwait(false));
        }
    }
}