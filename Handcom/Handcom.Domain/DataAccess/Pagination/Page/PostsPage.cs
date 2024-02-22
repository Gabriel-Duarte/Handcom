using Handcom.Domain.DataAccess.Pagination.Base;

namespace Handcom.Domain.DataAccess.Pagination.Page
{
    public class PostsPage : Pageable
    {
        public Guid? Topic { get; set; }
    }
}
