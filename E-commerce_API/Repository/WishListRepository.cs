using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class WishListRepository : IWishListRepository
    {
        Context context;
        public WishListRepository(Context context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<WishList> GetAll()
        {
            return context.wishLists
                .Include(p => p.customer)
                .Include(p => p.product)
                .ToList();
        }

        public List<WishList> GetAllbyCustomerId(string id)
        {
            List<WishList> wishLists = context.wishLists.
                Where(item => item.Customer_Id == id && item.IsDeleted == false).ToList();
            return wishLists;
        }

        public WishList GetById(int id)
        {
            return context.wishLists
                .Include(p => p.product)
                .Include(p => p.customer)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Insert(WishList obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(WishList obj)
        {
            context.Update(obj);
        }
    }
}
