using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Handcom.Domain.Models
{
    [Table("Posts")]
    public class Posts : Entity
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [Column(TypeName = "varchar(30)")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "O titulo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo conteudo é obrigatório.")]
        [Column(TypeName = "varchar(500)")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "O conteudo deve ter no máximo {1} caracteres e no mínimo {2}.")]
        public string Content { get; set; }

        [Column(TypeName = "varchar(Max)")]
        public string? ContentImage { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(Topics))]
        public Guid TopicId { get; set; }

        [Required(ErrorMessage = "O campo Inserido é obrigatório.")]
        public DateTime CreatedAt { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Topics? Topics { get; set; }

        public virtual List<Comments> Comments { get; set; } = new List<Comments>();
    }
}
