using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Database;
using Preethu.Phone.API.Models;
using System.Linq;

namespace Preethu.Phone.API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        SmartPhoneDbContext smartPhoneDbContext;
        public ManufacturerRepository(SmartPhoneDbContext smartPhoneDbContext)
        {
            this.smartPhoneDbContext = smartPhoneDbContext;
        }
        public bool CreateManufacturer(Manufacturer newManufacturer)
        {
            // Check for duplicate by name (case-insensitive)
            var existing = smartPhoneDbContext.TblManufacturer
                .FirstOrDefault(x => x.Name.ToLower() == newManufacturer.Name.ToLower());
            
            if (existing != null)
            {
                return false; // Duplicate found
            }
            
            smartPhoneDbContext.TblManufacturer.Add(newManufacturer);
            smartPhoneDbContext.SaveChanges();
            return true; // Successfully created
        }

        public bool Delete(int id)
        {
            Manufacturer? toBeDeleted = smartPhoneDbContext.TblManufacturer.FirstOrDefault(x => x.MId == id);
            if (toBeDeleted != null)
            {
                smartPhoneDbContext.TblManufacturer.Remove(toBeDeleted);
                smartPhoneDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Manufacturer? GetById(int id)
        {
            return smartPhoneDbContext.TblManufacturer.FirstOrDefault(x => x.MId == id);
        }

        public List<Manufacturer> GetManufacturers()

        {
            return smartPhoneDbContext.TblManufacturer.ToList();
        }

        public bool Update(int id, Manufacturer manufacturer)
        {
            Manufacturer? toBeUpdated = smartPhoneDbContext.TblManufacturer.FirstOrDefault(x => x.MId == id);
            if (toBeUpdated != null)
            {
                toBeUpdated.Name = manufacturer.Name;
                smartPhoneDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
