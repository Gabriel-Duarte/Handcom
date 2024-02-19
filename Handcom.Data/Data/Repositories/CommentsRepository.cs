using Handcom.Data.Data.Repositories.Base;
using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Data.Data.Repositories
{
    public class CommentsRepository : Repository<Comments>, ICommentsRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsRepository(IUnitOfWork uow, AppDbContext context, UserManager<ApplicationUser> userManager) : base(uow, context)
        {
            _userManager = userManager;
        }

        public async Task<Page<CommentsResponseDto>> GetCommentsAsync(CommentsPage commentsPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context().Comments.AsQueryable();
                ListCommentsWhere(commentsPage, ref queryData);
                ListCommentsOrderBy(commentsPage, ref queryData);

                var content = await PaginateAsync(queryData, commentsPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);
               
                var result = content.Select(x => new CommentsResponseDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    AuthorId = x.AuthorId,
                    PostId = x.PostId,
                    CreatedAt = x.CreatedAt,
                }).ToList();
                foreach (var comment in result)
                {
                    var user = await _userManager.FindByIdAsync(comment.AuthorId.ToString());
                    comment.AuthorName = user.UserName;
                }
                return new Page<CommentsResponseDto>(total, result, commentsPage);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private static void ListCommentsWhere(CommentsPage topicsPage, ref IQueryable<Comments> queryData)
        {
            if (!string.IsNullOrWhiteSpace(topicsPage.Search))
                queryData = queryData
                    .Where(s => s.Content.ToUpper().Contains(topicsPage.Search.ToUpper()) ||
                    s.PostId.ToString() == topicsPage.Search);
        }

            private static void ListCommentsOrderBy(CommentsPage topicsPage, ref IQueryable<Comments> queryData)
        {
            queryData = topicsPage.Sort switch
            {
                "name" => topicsPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.CreatedAt) : queryData.OrderByDescending(o => o.CreatedAt),
                _ => queryData.OrderBy(o => o.CreatedAt),
            };
        }
    }
}

