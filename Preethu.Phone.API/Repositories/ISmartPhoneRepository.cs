using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Repositories
{
    public interface ISmartPhoneRepository
    {
        List<SmartPhone> GetAll();
        SmartPhone GetById(int id);
        string Create(SmartPhone specs);
        bool Update(int id, SmartPhone spec);
        bool Delete(int id);
        List<SmartPhone>? GetBySpecs(SearchQuery searchQuery);
        List<SmartPhone>? GetByManufacturer(string name);
    }
}
