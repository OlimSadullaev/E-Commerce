using Microsoft.AspNetCore.Identity;

namespace E_commerce_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Shipment>? shipments { get; set; }
        public List<Payment>? payments { get; set; }
        public List<Cart>? carts { get; set; }
        public List<WishList>? wishlist { get; set; }
    }
}
