using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_API.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public Double Amount { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("customer")]
        public string Customer_Id { get; set; }
        public ApplicationUser customer { get; set; }
    }
}
