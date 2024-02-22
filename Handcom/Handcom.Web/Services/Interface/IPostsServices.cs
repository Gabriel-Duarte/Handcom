using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Pagination.Base;

namespace Handcom.Web.Services.Interface
{
    public interface IPostsServices
    {
        Task<Response<Page<PostsResponse>>> GetListPosts(PostsRequest postsRequest);
        Task<Response<PostsResponse>> CreatePosts(PostsCreateRequest postsCreateRequestDto);
    }
}
