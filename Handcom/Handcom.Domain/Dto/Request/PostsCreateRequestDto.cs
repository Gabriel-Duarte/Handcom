using System.ComponentModel.DataAnnotations;

namespace Handcom.Domain.Dto.Request
{
    public class PostsCreateRequestDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "O titulo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "O conteudo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Content { get; set; } = string.Empty;

        public string? ContentImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo topicid é obrigatório.")]
        public Guid TopicId { get; set; }
    }
}
