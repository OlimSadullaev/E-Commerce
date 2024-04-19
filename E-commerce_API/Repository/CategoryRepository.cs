using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        Context context;
        public CategoryRepository(Context context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Category crs = GetById(id);
            context.Remove(crs);
        }

        public List<Category> GetAll()
        {
            return context.categories.Include(c => c.products).ToList();
        }

        public Category GetById(int id)
        {
            return context.categories
                 .Include(c => c.products)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Insert(Category obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Category obj)
        {
            context.Update(obj);
        }
    }
}
