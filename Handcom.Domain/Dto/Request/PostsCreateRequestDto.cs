using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Request
{
    public class PostsCreateRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ContentImage { get; set; }
        public Guid TopicId { get; set; }
    }
}
