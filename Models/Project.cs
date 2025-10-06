using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundsy_backend.Models
{
    public class Project
    {
        [Key]
        [Column(TypeName = "binary(16)")]
        public Guid Id { get; set; }
        [Column(TypeName = "longtext")]
        public string Title { get; set; } = String.Empty;
        [Column(TypeName = "longtext")]
        public string Description { get; set; } = String.Empty;
        [Column(TypeName = "int")]
        public int Category_id { get; set; }
        [Column(TypeName = "binary(16)")]
        public Guid User_id { get; set; }
        [Column(TypeName = "binary(16)")]
        public Guid Payment_id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount_Incurred { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Goal_Amount { get; set; }
        [Column(TypeName = "longtext")]
        public string Image_Url { get; set; } = String.Empty;
        [Column(TypeName = "datetime")]
        public DateTime Start_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End_Date { get; set; }
        [Column(TypeName = "int")]
        public int Status_id { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}