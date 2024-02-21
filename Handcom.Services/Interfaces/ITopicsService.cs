using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;

namespace Handcom.Services.Interfaces
{
    public interface ITopicsService
    {
        Task<Topics> CreateAsync(TopicsCreateRequestDto topicsCreateRequestDto, CancellationToken cancellationToken);
        Task<Page<Topics>> GetAsync(TopicsPage pagination, CancellationToken cancellationToken);
        Task<Topics> UpdateAsync(TopicsUpdateRequestDto topicsCreateRequestDto, CancellationToken cancellationToken);
    }
}
