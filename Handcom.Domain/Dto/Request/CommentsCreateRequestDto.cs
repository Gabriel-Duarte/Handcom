using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Request
{
    public class CommentsCreateRequestDto
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
