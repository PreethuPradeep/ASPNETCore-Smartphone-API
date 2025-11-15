using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Repositories
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetManufacturers();
        Manufacturer GetById(int id);
        bool CreateManufacturer(Manufacturer newManufacturer);
        bool Update(int id, Manufacturer manufacturer);
        bool Delete(int id);
    }
}
