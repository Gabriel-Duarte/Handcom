using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Responses
{
    public class UserProfileResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
    }
}
