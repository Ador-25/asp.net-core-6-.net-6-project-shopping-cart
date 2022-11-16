using Microsoft.AspNetCore.Identity;

namespace ShoppingCart.Models
{
        public class AppUser : IdentityUser
        {
             public string Address { get; set; }
        }
}
