using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for User
        public string UserId { get; set; }

        // Foreign key for MenuItem
        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        // Quantity of the menu item in the cart
        public int Quantity { get; set; } = 1;

    }

}
