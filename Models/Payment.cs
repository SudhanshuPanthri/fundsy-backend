using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundsy_backend.Models
{
    public class Payment
    {
        [Key]
        [Column(TypeName = "binary(16)")]
        public Guid Id { get; set; }
        [Column(TypeName = "binary(16)")]
        public Guid User_id { get; set; }
        [Column(TypeName = "binary(16)")]
        public Guid Project_id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "int")]
        public int Payment_status_id { get; set; }
        [Column(TypeName = "longtext")]
        public string Payment_Type { get; set; } = String.Empty;
        public User User { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}