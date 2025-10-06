using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundsy_backend.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "binary(16)")]
        public Guid Id { get; set; }
        [Column(TypeName = "longtext")]
        public string UserName { get; set; } = String.Empty;
        [Column(TypeName = "longtext")]
        public string Email { get; set; } = String.Empty;
        [Column(TypeName = "longtext")]
        public string PasswordHash { get; set; } = String.Empty;
        [Column(TypeName = "int")]
        public int Role_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
