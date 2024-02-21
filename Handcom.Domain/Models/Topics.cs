using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handcom.Domain.Models
{
    [Table("Topics")]
    public class Topics : Entity
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [Column(TypeName = "varchar(20)")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O titulo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Name { get; set; }
    }
}
