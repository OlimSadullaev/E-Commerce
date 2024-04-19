using E_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_API.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        Context context;

        // inject Context
        public ShipmentRepository(Context context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Shipment shipment = GetById(id);
            context.Remove(shipment);
        }

        public List<Shipment> GetAll()
        {
            return context.shipments.Include(s => s.customer).ToList();
        }

        public Shipment GetById(int id)
        {
            return context.shipments
                .FirstOrDefault(p => p.Id == id);
        }

        public void Insert(Shipment obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Shipment obj)
        {
            context.Update(obj);
        }
    }
}
