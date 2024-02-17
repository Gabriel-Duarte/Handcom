using Handcom.Domain.Dto.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Responses
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; } = null!;
    }
}
