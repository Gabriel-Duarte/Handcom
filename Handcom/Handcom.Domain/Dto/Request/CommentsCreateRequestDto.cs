using System.ComponentModel.DataAnnotations;

namespace Handcom.Domain.Dto.Request
{
    public class CommentsCreateRequestDto
    {
        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo postid é obrigatório.")]
        public Guid PostId { get; set; }
    }
}
