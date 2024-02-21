namespace Handcom.Domain.Dto.Responses
{
    public class CommentsResponseDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
