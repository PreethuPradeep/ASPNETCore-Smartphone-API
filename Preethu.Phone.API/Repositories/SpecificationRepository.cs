using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Database;
using Preethu.Phone.API.Models;
using System.Linq;

namespace Preethu.Phone.API.Repositories
{
    public class SpecificationRepository : ISpecificationRepository
    {
            SmartPhoneDbContext smartPhoneDbContext;
            public SpecificationRepository(SmartPhoneDbContext smartPhoneDbContext)
            {
                this.smartPhoneDbContext = smartPhoneDbContext;
            }
            public bool Create(SmartPhoneSpec specs)
            {
                // Check for duplicate by all specification fields (case-insensitive)
                var existing = smartPhoneDbContext.TblSpecification
                    .FirstOrDefault(x => 
                        x.Processor.ToLower() == specs.Processor.ToLower() &&
                        x.RAM.ToLower() == specs.RAM.ToLower() &&
                        x.Storage.ToLower() == specs.Storage.ToLower() &&
                        x.OS.ToLower() == specs.OS.ToLower());
                
                if (existing != null)
                {
                    return false; // Duplicate found
                }
                
                smartPhoneDbContext.TblSpecification.Add(specs);
                smartPhoneDbContext.SaveChanges();
                return true; // Successfully created
            }

            public bool Delete(int id)
            {
                SmartPhoneSpec? toBeDeleted = smartPhoneDbContext.TblSpecification.FirstOrDefault(x => x.SpecId == id);
                if (toBeDeleted != null)
                {
                    smartPhoneDbContext.TblSpecification.Remove(toBeDeleted);
                    smartPhoneDbContext.SaveChanges();
                    return true;
                }
                return false;
            }

            public List<SmartPhoneSpec> GetSpecifications()
            {
                return smartPhoneDbContext.TblSpecification.ToList();
            }

            public SmartPhoneSpec? GetById(int id)
            {
                return smartPhoneDbContext.TblSpecification.FirstOrDefault(x => x.SpecId == id);
            }

            public bool Update(int id, SmartPhoneSpec spec)
            {
                var toBeUpdated = smartPhoneDbContext.TblSpecification.FirstOrDefault(x => x.SpecId == id);
                if (toBeUpdated != null)
                {
                    toBeUpdated.Processor = spec.Processor;
                    toBeUpdated.RAM = spec.RAM;
                    toBeUpdated.Storage = spec.Storage;
                    toBeUpdated.OS = spec.OS;
                    smartPhoneDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
    }
}

