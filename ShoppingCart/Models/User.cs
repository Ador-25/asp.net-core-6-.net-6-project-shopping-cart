using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
        public class User
        {

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
                [Display(Name = "Username")]
                [Key]
                public string UserName { get; set; }
                [Required, EmailAddress]
                public string Email { get; set; }
                [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
                public string Password { get; set; }
                [StringLength(11)]
                public string PhoneNumber { get; set; }
                public string Address { get; set; }


    }
    
}