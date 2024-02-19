namespace Handcom.Web.Model.Request
{
    public class CommentsCreateRequest
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
