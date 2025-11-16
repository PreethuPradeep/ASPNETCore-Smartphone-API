using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Database;
using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Repositories
{
    public class SmartPhoneRepository : ISmartPhoneRepository
    {
        SmartPhoneDbContext smartPhoneDbContext;
        public SmartPhoneRepository(SmartPhoneDbContext smartPhoneDbContext)
        {
            this.smartPhoneDbContext = smartPhoneDbContext;
        }
        public string Create(SmartPhone smartPhone)
        {
            var existingName = smartPhoneDbContext.TblSmartPhone
                .FirstOrDefault(x => x.Name.ToLower() == smartPhone.Name.ToLower());
            if (existingName != null)
            {
                return "duplicate name";
            }
            var manufacturerExists = smartPhoneDbContext.TblManufacturer
                .Any(m => m.MId == smartPhone.MId);
            if (!manufacturerExists)
            {
                return "invalid manufacturer";
            }
            var specExists = smartPhoneDbContext.TblSpecification
                .Any(s => s.SpecId == smartPhone.SpecId);
            if (!specExists)
            {
                return "invalid specification";
            }
            smartPhoneDbContext.TblSmartPhone.Add(smartPhone);
            smartPhoneDbContext.SaveChanges();
            return "success";
        }

        public bool Delete(int id)
        {
            SmartPhone? toBeDeleted = smartPhoneDbContext.TblSmartPhone.FirstOrDefault(x => x.Id == id);
            if (toBeDeleted != null)
            {
                smartPhoneDbContext.TblSmartPhone.Remove(toBeDeleted);
                smartPhoneDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<SmartPhone> GetAll()
        {
            return smartPhoneDbContext.TblSmartPhone.Include(x => x.Manufacturer).Include(x => x.Specification).ToList();
        }

        public SmartPhone? GetById(int id)
        {
            return smartPhoneDbContext.TblSmartPhone.Include(s => s.Manufacturer)
                    .Include(s => s.Specification).FirstOrDefault(x => x.Id == id);
        }

        public List<SmartPhone>? GetByManufacturer(string name)
        {
            return smartPhoneDbContext.TblSmartPhone
                .Include(s => s.Manufacturer)
                .Include(s => s.Specification)
                .Where(p => p.Manufacturer != null && p.Manufacturer.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<SmartPhone>? GetBySpecs(SearchQuery searchQuery)
        {
            var SmartPhones = smartPhoneDbContext.TblSmartPhone
                .Include(s => s.Manufacturer)
                .Include(s => s.Specification)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery.Processor))
            {
                SmartPhones = SmartPhones.Where(p => p.Specification != null && p.Specification.Processor != null && p.Specification.Processor.ToLower().Contains(searchQuery.Processor.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchQuery.RAM))
            {
                SmartPhones = SmartPhones.Where(p => p.Specification != null && p.Specification.RAM != null && p.Specification.RAM.ToLower().Contains(searchQuery.RAM.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchQuery.OS))
            {
                SmartPhones = SmartPhones.Where(p => p.Specification != null && p.Specification.OS != null && p.Specification.OS.ToLower().Contains(searchQuery.OS.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchQuery.Storage))
            {
                SmartPhones = SmartPhones.Where(p => p.Specification != null && p.Specification.Storage != null && p.Specification.Storage.ToLower().Contains(searchQuery.Storage.ToLower()));
            }
            return SmartPhones.ToList();
        }

        public bool Update(int id, SmartPhone phone)
        {
            var toBeUpdated = smartPhoneDbContext.TblSmartPhone.FirstOrDefault(x => x.Id == id);
            if (toBeUpdated != null)
            {
                toBeUpdated.Name = phone.Name;
                toBeUpdated.Description = phone.Description;
                toBeUpdated.Price = phone.Price;
                toBeUpdated.MId = phone.MId;
                toBeUpdated.SpecId = phone.SpecId;
                smartPhoneDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
