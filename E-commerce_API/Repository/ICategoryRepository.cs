using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Insert(Category obj);
        void Update(Category obj);
        void Delete(int id);
        void Save();
    }
}
