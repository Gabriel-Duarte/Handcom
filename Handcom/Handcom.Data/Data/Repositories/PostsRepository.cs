﻿using Handcom.Data.Data.Repositories.Base;
using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Handcom.Data.Data.Repositories
{
    public class PostsRepository : Repository<Posts>, IPostsRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public PostsRepository(IUnitOfWork uow, AppDbContext context, UserManager<ApplicationUser> userManager) : base(uow, context)
        {
            _userManager = userManager;
        }

        public async Task<Page<PostsResponseDto>> GetPostsAsync(PostsPage PostsPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context().Posts.AsQueryable();
                ListPostWhere(PostsPage, ref queryData);
                ListPostOrderBy(PostsPage, ref queryData);

                var content = await PaginateAsync(queryData, PostsPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);

                var authorIds = content.Select(x => x.AuthorId.ToString()).Distinct().ToList();
                var users = await _userManager.Users.Where(u => authorIds.Contains(u.Id))
                                                    .ToDictionaryAsync(u => u.Id, u => u);

                var result = content.Select(x => new PostsResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ContentImage = x.ContentImage,
                    CreatedAt = x.CreatedAt,
                    TopicId = x.TopicId,
                    AuthorId = x.AuthorId,
                    Author = users[x.AuthorId].UserName,
                    AuthorImage = users[x.AuthorId].ImagePath
                }).ToList();

                return new Page<PostsResponseDto>(total, result, PostsPage);
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
