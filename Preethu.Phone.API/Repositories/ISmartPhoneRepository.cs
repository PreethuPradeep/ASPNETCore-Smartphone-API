using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Repositories
{
    public interface ISmartPhoneRepository
    {
        List<SmartPhone> GetAll();
        SmartPhone GetById(int id);
        void Create(SmartPhone specs);
        bool Update(int id, SmartPhone spec);
        bool Delete(int id);
        public List<SmartPhone> Search(SearchQuery filter);
        SmartPhone? GetByName(string name);
    }
}
