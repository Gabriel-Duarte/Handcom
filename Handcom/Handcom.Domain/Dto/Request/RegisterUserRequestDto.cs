using System.ComponentModel.DataAnnotations;


namespace Handcom.Domain.Dto.Request
{
    public class RegisterUserRequestDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string? Username { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string? Password { get; set; } = string.Empty;
    }
}

