using System.ComponentModel.DataAnnotations;

namespace Handcom.Domain.Dto.Request
{
    public class UpdateUserProfileRequestDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string UserName { get; set; } = string.Empty;

        public string? ImagePath { get; set; } = string.Empty;
    }
}
