using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Pagination.Base;

namespace Handcom.Web.Services.Interface
{
    public interface ITopicsService
    {
        Task<Response<Page<TopicsResponse>>> GetListTopics(TopicsRequest topicsRequest);
        Task<Response<TopicsResponse>> TopicCreate(TopicCreateRequest topicCreateRequest);
        Task<Response<TopicsResponse>> UpdateTopic(TopicsUpdateRequest topicsUpdateRequest);
    }
}
