using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Insert(Product obj);
        void Update(Product obj);
        void DeleteById(int id);
        void Save();
    }
}
