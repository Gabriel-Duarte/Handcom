namespace Handcom.Web.Model.Responses
{
    public class CommentsResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
