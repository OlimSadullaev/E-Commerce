using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface IWishListRepository
    {
        List<WishList> GetAll();
        WishList GetById(int id);
        void Insert(WishList obj);
        void Update(WishList obj);
        void Delete(int id);
        void Save();
        public List<WishList> GetAllbyCustomerId(string id);
    }
}
