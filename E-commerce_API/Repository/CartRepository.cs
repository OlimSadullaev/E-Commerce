using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class CartRepository : ICartRepository
    {
        Context context;
        public CartRepository(Context context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Cart crs = GetById(id);
            crs.IsDeleted = true;
            Update(crs);
        }

        public List<Cart> GetAll()
        {
            return context.carts
                 .Include(c => c.product)
                 .Include(c => c.customer)
                 .Where(c => !c.IsDeleted)
                 .ToList();

        }

        public Cart GetById(int id)
        {
            return context.carts
                 .Include(c => c.product)
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        }

        public List<Cart> GetCartItemsOfCustomer(string customerId)
        {
            return GetAll().Where(items => items.Customer_id == customerId).ToList();
        }

        public int GetTotalPrice(string customerId)
        {
            int totalPrice = 0;
            foreach(Cart cart in GetCartItemsOfCustomer(customerId)) 
            {
                totalPrice += (cart.product.Price * cart.Quantity);
            }

            return totalPrice;
        }

        public void Insert(Cart obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Cart obj)
        {
            context.Update(obj);
        }
    }
}
