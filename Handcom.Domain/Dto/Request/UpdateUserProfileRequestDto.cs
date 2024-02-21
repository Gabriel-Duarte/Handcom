using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Request
{
    public class UpdateUserProfileRequestDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string UserName { get; set; }
        public string? ImagePath { get; set; }
    }
}
