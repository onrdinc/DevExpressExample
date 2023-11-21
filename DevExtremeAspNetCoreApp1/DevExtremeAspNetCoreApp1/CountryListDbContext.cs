

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevExtremeAspNetCoreApp1
{
    public class CountryListDbContext : DbContext
    {
        public CountryListDbContext(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-R04PVQ3\\SQLEXPRESS; Database = CountryListDb; Trusted_Connection = true; TrustServerCertificate = true;");
        }
    }
}
