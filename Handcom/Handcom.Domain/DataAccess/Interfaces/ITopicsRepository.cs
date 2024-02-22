using Handcom.Domain.DataAccess.Interfaces.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Models;

namespace Handcom.Domain.DataAccess.Interfaces
{
    public interface ITopicsRepository : IRepository<Topics>
    {
        Task<Page<Topics>> GetTopicsAsync(TopicsPage topicsPage, CancellationToken cancellationToken);
    }
}
