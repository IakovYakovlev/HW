using HW_17.Models.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HW_17.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration Configuration) => services
            .AddDbContext<SQLContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MSSQL")))
            .AddDbContext<AccessContext>(opt => opt.UseJet(Configuration.GetConnectionString("AccessData")));
    }
}

