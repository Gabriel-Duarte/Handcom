using Handcom.Data.Data.Repositories.Base;
using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Data.Data.Repositories
{
    public class PostsRepository : Repository<Posts>, IPostsRepository
    {
        private const int GET_TEN_ITEMS = 10;
        private const int GET_TWENTY_ITEMS = 20;

        public PostsRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }

        public async Task<Page<Posts>> GetPostsAsync(PostsPage PostsPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context().Posts.AsQueryable();
                ListPostWhere(PostsPage, ref queryData);
                ListPostOrderBy(PostsPage, ref queryData);

                var content = await PaginateAsync(queryData, PostsPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);

                return new Page<Posts>(total, content, PostsPage);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private static void ListPostWhere(PostsPage PostsPage, ref IQueryable<Posts> queryData)
        {
            if (!string.IsNullOrWhiteSpace(PostsPage.Search))
                queryData = queryData
                    .Where(s => s.Content.ToUpper().Contains(PostsPage.Search.ToUpper()) ||
                    s.AuthorId.Contains(PostsPage.Search));

            if (PostsPage.Topic.HasValue && PostsPage.Topic.Value != Guid.Empty)
                queryData = queryData.Where(s => s.TopicId == PostsPage.Topic);

        }

            private static void ListPostOrderBy(PostsPage PostsPage, ref IQueryable<Posts> queryData)
        {
            queryData = PostsPage.Sort switch
            {
                "name" => PostsPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.CreatedAt) : queryData.OrderByDescending(o => o.CreatedAt),
                _ => queryData.OrderBy(o => o.CreatedAt)
            };
    }
}
}
