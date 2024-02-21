using System.ComponentModel.DataAnnotations;

namespace Handcom.Domain.Dto.Request
{
    public class TopicsUpdateRequestDto
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;
    }
}
