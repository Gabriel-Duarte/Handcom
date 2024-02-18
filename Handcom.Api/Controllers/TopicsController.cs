﻿using Handcom.Api.Controllers.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Responses;
using Handcom.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Handcom.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopicsController : MainController
    {
        private readonly ITopicsService _topicsService;

        public TopicsController(INotifierService notifierService, ITopicsService topicsService) : base(notifierService)
        {
            _topicsService = topicsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Topics>>> Get([FromQuery] TopicsPage topicsPage) =>
           CustomResponse(await _topicsService.GetAsync(topicsPage, CancellationToken.None).ConfigureAwait(false));


        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Topics>> Create(TopicsCreateRequestDto companyRequestDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _topicsService.CreateAsync(companyRequestDto, CancellationToken.None).ConfigureAwait(false));
        }
    }

}