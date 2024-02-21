using System.ComponentModel.DataAnnotations;

namespace Handcom.Domain.Dto.Request
{
    public class TopicsCreateRequestDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;
    }
}
