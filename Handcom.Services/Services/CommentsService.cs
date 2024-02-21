using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Handcom.Services.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        private readonly ILogger<CommentsService> _logger;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsService(
           INotifierService notifierService,
           ILogger<CommentsService> logger,
           ICommentsRepository commentsRepository,
           IMapper mapper,
           IHttpContextAccessor httpContextAccessor) : base(notifierService)
        {
            _logger = logger;
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Page<CommentsResponseDto>> GetAsync(CommentsPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _commentsRepository.GetCommentsAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CompanyService, GetAsync: Request object => {SerializeObjectToJson(pagination)}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Page<CommentsResponseDto>());
            }
        }

        public async Task<Comments> CreateAsync(CommentsCreateRequestDto commentsCreateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(commentsCreateRequestDto.Content))
                    return Notify("Conteudo não encontrado.", new Comments());

                if (commentsCreateRequestDto.PostId == Guid.Empty)
                    return Notify("Post nao encontrado não encontrado.", new Comments());

                var comments = _mapper.Map<Comments>(commentsCreateRequestDto);
                comments.AuthorId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                comments.CreatedAt = DateTime.Now;

                return await _commentsRepository.CreateAsync(comments, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Comments());
            }
        }
    }
}
