namespace Handcom.Domain.Dto.Responses
{
    public class PostsResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ContentImage { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public string AuthorImage { get; set; } = string.Empty;
        public Guid TopicId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
