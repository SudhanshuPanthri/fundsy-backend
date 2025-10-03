using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundsy_backend.Models
{
    public class User
    {
        [Key]
        [Column(TypeName ="binary(16)")]
        public Guid Id { get; set; }
        [Column(TypeName = "longtext")]
        public string UserName { get;set; } = String.Empty;
        [Column(TypeName = "longtext")]
        public string Email { get; set; } = String.Empty;
        [Column(TypeName = "longtext")]
        public string PasswordHash { get; set; } = String.Empty;
    }
}
