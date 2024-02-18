using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<Comments> CreateAsync(CommentsCreateRequestDto CommentsCreateRequestDto, CancellationToken cancellationToken);
        Task<Page<Comments>> GetAsync(CommentsPage pagination, CancellationToken cancellationToken);
    }
}
