using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.ViewModels
{
        public class CartViewModel
        {
        [Key]
        public Guid id { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }

        public bool LocationIsDhaka { get; set; }
        public bool LocationOutsideDhaka { get; set; }
        public Location DeliveryLocation { get; set; }
        public bool OrderCompleted { get; set; }
        public string DeliveryCharge { get; set; } = "Inside Dhaka: BDT 60 ,  Outside Dhaka BDT 130.";
    }
    public enum Location
    {
        Dhaka,
        Outside_Dhaka
    }
}