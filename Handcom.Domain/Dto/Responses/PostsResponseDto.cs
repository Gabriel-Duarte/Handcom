using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Responses
{
    public class PostsResponseDto
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
