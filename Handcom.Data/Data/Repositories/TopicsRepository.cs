using Handcom.Data.Data.Repositories.Base;
using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Handcom.Data.Data.Repositories
{
    public class TopicsRepository : Repository<Topics>, ITopicsRepository
    {
        private const int GET_TEN_ITEMS = 10;
        private const int GET_TWENTY_ITEMS = 20;

        public TopicsRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }
      
        public async Task<Page<Topics>> GetTopicsAsync(TopicsPage topicsPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context().Topics.AsQueryable();
                ListTopicsWhere(topicsPage, ref queryData);
                ListTopicsOrderBy(topicsPage, ref queryData);

                var content = await PaginateAsync(queryData, topicsPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);

                return new Page<Topics>(total, content, topicsPage);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private static void ListTopicsWhere(TopicsPage topicsPage, ref IQueryable<Topics> queryData)
        {
            if (!string.IsNullOrWhiteSpace(topicsPage.Search))
                queryData = queryData
                    .Where(s => s.Name.ToUpper().Contains(topicsPage.Search.ToUpper()));
        }

        private static void ListTopicsOrderBy(TopicsPage topicsPage, ref IQueryable<Topics> queryData)
        {
            queryData = topicsPage.Sort switch
            {
                "name" => topicsPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Name) : queryData.OrderByDescending(o => o.Name),
                _ => queryData.OrderBy(o => o.Name),
            };
        }
    }
}
