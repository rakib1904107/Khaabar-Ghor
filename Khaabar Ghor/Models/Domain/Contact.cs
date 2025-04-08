using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class Contact
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
