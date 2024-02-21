using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Request
{
    public class CommentsCreateRequestDto
    {
        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "O campo postid é obrigatório.")]
        public Guid PostId { get; set; }
    }
}
