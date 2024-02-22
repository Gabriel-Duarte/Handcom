using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Request
{
    public class CommentsCreateRequest
    {
        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "O campo postid é obrigatório.")]
        public Guid PostId { get; set; }
    }
}
