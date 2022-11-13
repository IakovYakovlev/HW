using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection service, IConfiguration Configuration) => service
            .AddDbContext<PhoneBookContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MSSQL")))
            ;
    }
}
