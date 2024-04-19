using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        Context context;

        public ProductRepository(Context context) 
        {
            this.context = context;
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return context.products
                .Include(p => p.category)
                .ToList();
        }

        public Product GetById(int id)
        {
            return context.products
                .Include(p => p.category)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Insert(Product obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Product obj)
        {
            context.Update(obj);
        }
    }
}
