using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface IPaymentRepository
    {
        List<Payment> GetAll();
        Payment GetById(int id);
        void Insert(Payment obj);
        void Update(Payment obj);
        void Delete(int id);
        void Save();
    }
}
