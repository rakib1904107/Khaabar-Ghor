using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}