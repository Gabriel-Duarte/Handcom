using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Extensions
{
    public class UserTokenDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserImage { get; set; } = null!;
    }
}
