using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}