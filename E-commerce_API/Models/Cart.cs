using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("customer")]
        public string Customer_id { get; set; }

        public ApplicationUser customer { get; set; }

        [ForeignKey("product")]
        public int Product_Id { get; set; }
        public Product? product { get; set; }
    }
}
