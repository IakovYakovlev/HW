using IdentityShared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Data
{
    public class PhoneBookContext : IdentityDbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> opt) : base(opt) { }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                UserManager<IdentityUser> userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));

                RoleManager<IdentityRole> roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

                string email = configuration["Admin:AdminUser:Email"];
                string password = configuration["Admin:AdminUser:Password"];
                string role = configuration["Admin:AdminUser:Role"];

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }

                    var user = new IdentityUser
                    {
                        Email = email,
                        UserName = email
                    };

                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }

                string userRole = configuration["Admin:UserRole:Role"];
                if (await roleManager.FindByNameAsync(userRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(userRole));
                }
            }
        }
    }
}
