using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        private readonly ILogger<CommentsService> _logger;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly IAspNetUserService _aspNetUserService;

        public CommentsService(
           INotifierService notifierService,
           ILogger<CommentsService> logger,
           ICommentsRepository commentsRepository,
           IAspNetUserService aspNetUserService,
           IMapper mapper) : base(notifierService)
        {
            _logger = logger;
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _aspNetUserService = aspNetUserService;
        }

        public async Task<Page<Comments>> GetAsync(CommentsPage pagination, CancellationToken cancellationToken)
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
                return Notify("Ocorreu um erro.", new Page<Comments>());
            }
        }

        public async Task<Comments> CreateAsync(CommentsCreateRequestDto commentsCreateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (commentsCreateRequestDto.Content is null)
                    return Notify("Conteudo não encontrado.", new Comments());

                if (commentsCreateRequestDto.PostId == Guid.Empty)
                    return Notify("Post nao encontrado não encontrado.", new Comments());

                var comments = _mapper.Map<Comments>(commentsCreateRequestDto);
                comments.AuthorId = _aspNetUserService.GetUserId().ToString();
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
