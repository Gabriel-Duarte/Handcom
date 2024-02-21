using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Request
{
    public class TopicsUpdateRequestDto
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public Guid Id { get; set; }    
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; }
    }
}
