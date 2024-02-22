using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Handcom.Domain.Models
{
    public class Comments : Entity
    {
        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        [Column(TypeName = "varchar(500)")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "O conteudo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Content { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(Posts))]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "O campo Inserido é obrigatório.")]
        public DateTime CreatedAt { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Posts? Posts { get; set; }

    }
}
