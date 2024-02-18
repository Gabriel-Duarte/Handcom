using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        [Column(TypeName = "varchar(Max)")]
        public string? ImagePath { get; set; } = null!;
    }
}
