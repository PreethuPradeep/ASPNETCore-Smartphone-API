using Preethu.Phone.API.Database;
using Preethu.Phone.API.Models;
using Preethu.Phone.API.Repositories;

namespace Preethu.Phone.API.Services
{
    public class SmartPhoneService : ISmartPhoneService
    {
        ISmartPhoneRepository smartPhoneRepository;
        IManufacturerRepository manufacturerRepository;
        ISpecificationRepository specRepo;
        public SmartPhoneService(ISmartPhoneRepository smartPhoneRepository,
            IManufacturerRepository manufacturerRepository,
            ISpecificationRepository specRepo)
        {
            this.smartPhoneRepository = smartPhoneRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.specRepo = specRepo;
        }
        public string Add(SmartPhone smartPhone)
        {
            if (smartPhoneRepository.GetByName(smartPhone.Name) != null)
            {
                return "duplicate_name";
            }

            if (manufacturerRepository.GetById(smartPhone.MId) == null)
            {
                return "invalid_manufacturer";
            }

            // 3. Check for valid Specification
            if (specRepo.GetById(smartPhone.SpecId) == null)
            {
                return "invalid_specification";
            }
            smartPhoneRepository.Create(smartPhone);
            return "success";
        }
    }
}
