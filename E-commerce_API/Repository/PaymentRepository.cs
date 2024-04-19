using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        Context context;
        public PaymentRepository(Context context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            Payment crs = GetById(id);
            context.Remove(crs);
        }

        public Payment GetById(int id)
        {
            return context.payments
                 .Include(c => c.customer)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Payment> GetAll()
        {
            return context.payments.Include(c => c.customer).ToList();
        }

        public void Insert(Payment obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Payment obj)
        {
            context.Update(obj);
        }
    }
}
