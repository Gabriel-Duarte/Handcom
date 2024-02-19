namespace Handcom.Web.Model.Request
{
    public class PostsCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ContentImage { get; set; }
        public Guid TopicId { get; set; }
    }
}
