using Handcom.Domain.DataAccess.Interfaces.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;


namespace Handcom.Domain.DataAccess.Interfaces
{
    public interface IPostsRepository : IRepository<Posts>
    {
        Task<Page<PostsResponseDto>> GetPostsAsync(PostsPage PostsPage, CancellationToken cancellationToken);
    }
}
