using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface ICartRepository
    {
        List<Cart> GetAll();
        Cart GetById(int id);
        public int GetTotalPrice(string customerId);
        public List<Cart> GetCartItemsOfCustomer(string customerId);
        void Insert(Cart obj);
        void Update(Cart obj);
        void Delete(int id);
        void Save();
    }
}
