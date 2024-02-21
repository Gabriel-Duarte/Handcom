using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Request
{
    public class TopicsUpdateRequest
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; }
    }
}
