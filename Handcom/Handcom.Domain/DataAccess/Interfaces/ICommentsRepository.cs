using Handcom.Domain.DataAccess.Interfaces.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;

namespace Handcom.Domain.DataAccess.Interfaces
{
    public interface ICommentsRepository : IRepository<Comments>
    {
        Task<Page<CommentsResponseDto>> GetCommentsAsync(CommentsPage commentsPage, CancellationToken cancellationToken);
    }
}
