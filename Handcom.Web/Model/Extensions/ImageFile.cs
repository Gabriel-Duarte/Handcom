using System.ComponentModel.DataAnnotations;

namespace Handcom.Web.Model.Extensions
{
    public class ImageFile
    {
        [Required(ErrorMessage = "A Imagem é obrigatório.")]
        public string Base64 { get; set; } = null!;

        [Required(ErrorMessage = "O Nome do arquivo é obrigatório.")]
        public string FileName { get; set; } = null!;

    }
}
