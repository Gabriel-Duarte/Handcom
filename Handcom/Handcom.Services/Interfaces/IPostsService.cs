using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;


namespace Handcom.Services.Interfaces
{
    public interface IPostsService
    {
        Task<Posts> CreateAsync(PostsCreateRequestDto PostsCreateRequestDto, CancellationToken cancellationToken);
        Task<Page<PostsResponseDto>> GetAsync(PostsPage pagination, CancellationToken cancellationToken);
    }
}
