using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Request
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string? Password { get; set; }
    }
}
