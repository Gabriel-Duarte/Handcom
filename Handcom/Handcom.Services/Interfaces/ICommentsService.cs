﻿using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;

namespace Handcom.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<Comments> CreateAsync(CommentsCreateRequestDto CommentsCreateRequestDto, CancellationToken cancellationToken);
        Task<Page<CommentsResponseDto>> GetAsync(CommentsPage pagination, CancellationToken cancellationToken);
    }
}
