using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Pagination.Base;

namespace Handcom.Web.Services.Interface
{
    public interface ICommentsService
    {
        Task<Response<Page<CommentsResponse>>> GetListComments(CommentsRequest commentsRequest);
        Task<Response<CommentsResponse>> CreateComments(CommentsCreateRequest postsCreateRequestDto);
    }
}
