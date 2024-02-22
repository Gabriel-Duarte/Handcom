using Handcom.Web.Pagination.Base;

namespace Handcom.Web.Model.Request
{
    public class PostsRequest : Pageable
    {
        public Guid? Topic { get; set; }
    }
}
