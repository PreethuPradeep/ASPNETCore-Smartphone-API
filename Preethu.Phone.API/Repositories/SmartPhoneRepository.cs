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
        public void Create(SmartPhone smartPhone)
        {
            smartPhoneDbContext.TblSmartPhone.Add(smartPhone);
            smartPhoneDbContext.SaveChanges();
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


        public SmartPhone? GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return smartPhoneDbContext.TblSmartPhone
                .Include(s => s.Manufacturer)
                .Include(s => s.Specification)
                .FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public List<SmartPhone> Search(SearchQuery filter)
        {
            var query = smartPhoneDbContext.TblSmartPhone
                .Include(p => p.Manufacturer)
                .Include(p => p.Specification)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
                query = query.Where(p => p.Manufacturer.Name.Contains(filter.Manufacturer));

            if (!string.IsNullOrWhiteSpace(filter.Processor))
                query = query.Where(p => p.Specification.Processor.Contains(filter.Processor));

            if (!string.IsNullOrWhiteSpace(filter.Storage))
                query = query.Where(p => p.Specification.Storage == filter.Storage);

            if (!string.IsNullOrWhiteSpace(filter.Ram))
                query = query.Where(p => p.Specification.RAM == filter.Ram);

            if (!string.IsNullOrWhiteSpace(filter.Os))
                query = query.Where(p => p.Specification.OS == filter.Os);

            return query.ToList();
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
