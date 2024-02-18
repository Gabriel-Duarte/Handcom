﻿using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Extensions;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.Extensions.Logging;


namespace Handcom.Services.Services
{
    public class TopicsService : BaseService, ITopicsService
    {
        private readonly ILogger<TopicsService> _logger;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IMapper _mapper;

        public TopicsService(
           INotifierService notifierService,
           ILogger<TopicsService> logger,
           ITopicsRepository topicsRepository,
           IMapper mapper) : base(notifierService)
        {
            _logger = logger;
            _topicsRepository = topicsRepository;
            _mapper = mapper;
        }

        public async Task<Page<Topics>> GetAsync(TopicsPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _topicsRepository.GetTopicsAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CompanyService, GetAsync: Request object => {SerializeObjectToJson(pagination)}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Page<Topics>());
            }
        }

        public async Task<Topics> CreateAsync(TopicsCreateRequestDto topicsCreateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var topic = _mapper.Map<Topics>(topicsCreateRequestDto);
               
                return await _topicsRepository.CreateAsync(topic, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Topics());
            }
        }
    }
}
