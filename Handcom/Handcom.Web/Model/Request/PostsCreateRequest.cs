using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Request
{
    public class PostsCreateRequest
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "O titulo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "O conteudo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Content { get; set; }

        public string? ContentImage { get; set; }

        [Required(ErrorMessage = "O campo topicid é obrigatório.")]
        public Guid TopicId { get; set; }
    }
}
