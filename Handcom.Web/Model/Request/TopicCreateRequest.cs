using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Request
{
    public class TopicCreateRequest
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; }
    }
}
