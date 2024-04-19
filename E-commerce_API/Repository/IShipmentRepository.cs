using E_commerce_API.Models;

namespace E_commerce_API.Repository
{
    public interface IShipmentRepository
    {
        List<Shipment> GetAll();
        Shipment GetById(int id);
        void Insert(Shipment obj);
        void Update(Shipment obj);
        void Delete(int id);
        void Save();
    }
}
