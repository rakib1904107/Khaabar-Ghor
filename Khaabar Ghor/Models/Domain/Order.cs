using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";

    }
}
