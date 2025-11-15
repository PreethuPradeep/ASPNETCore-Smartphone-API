using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Repositories
{
    public interface ISpecificationRepository
    {
        List<SmartPhoneSpec> GetSpecifications();
        SmartPhoneSpec GetById(int id);
        bool Create(SmartPhoneSpec specs);
        bool Update(int id, SmartPhoneSpec spec);
        bool Delete(int id);
    }
}
