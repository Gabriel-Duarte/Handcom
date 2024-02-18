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
    public interface ITopicsService
    {
        Task<Topics> CreateAsync(TopicsCreateRequestDto topicsCreateRequestDto, CancellationToken cancellationToken);
        Task<Page<Topics>> GetAsync(TopicsPage pagination, CancellationToken cancellationToken);
    }
}
