namespace Handcom.Web.Model.Responses
{
    public class PostsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ContentImage { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string AuthorImage { get; set; }
        public Guid TopicId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
