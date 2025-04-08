using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Khaabar_Ghor.Models.Domain
{
    public class User : IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

    }
}