using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
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
                if (string.IsNullOrWhiteSpace(topicsCreateRequestDto.Name))
                    return Notify("Nome não encontrado.", new Topics());

                var topic = _mapper.Map<Topics>(topicsCreateRequestDto);

                return await _topicsRepository.CreateAsync(topic, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Topics());
            }
        }

        public async Task<Topics> UpdateAsync(TopicsUpdateRequestDto topicsUpdateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (string.IsNullOrWhiteSpace(topicsUpdateRequestDto.Name))
                    return Notify("Nome não encontrado.", new Topics());

                if (topicsUpdateRequestDto.Id == Guid.Empty)
                    return Notify("Id não encontrado.", new Topics());

                var resultTopic = await _topicsRepository.GetByIdAsync(topicsUpdateRequestDto.Id, cancellationToken);
                if (resultTopic is null)
                    return Notify("topico não encontrado.", new Topics());

                resultTopic.Name = topicsUpdateRequestDto.Name;

                return await _topicsRepository.UpdateAsync(resultTopic, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Topics());
            }
        }
    }
}
