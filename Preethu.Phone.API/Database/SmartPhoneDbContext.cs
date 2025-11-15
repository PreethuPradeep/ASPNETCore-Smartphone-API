using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Models;

namespace Preethu.Phone.API.Database
{
    public class SmartPhoneDbContext:IdentityDbContext<IdentityUser>
    {
        public SmartPhoneDbContext(DbContextOptions options):base(options)
        {  
        }
        public DbSet<Manufacturer> TblManufacturer { get; set; }
        public DbSet<SmartPhoneSpec> TblSpecification { get; set; }
        public DbSet<SmartPhone> TblSmartPhone { get; set; }
        public DbSet<SearchQuery> TblSearchQuery { get; set; }
    }
}
