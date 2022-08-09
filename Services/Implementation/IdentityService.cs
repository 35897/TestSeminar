using Microsoft.AspNetCore.Identity;
using TestSeminar.Models;
using TestSeminar.Models.Dbo;
using TestSeminar.Services.Interface;

namespace TestSeminar.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public IdentityService(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                this.userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                this.roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                CreateRoleAsync(Roles.Admin).Wait();
                CreateRoleAsync(Roles.BasicUser).Wait();

                //admin
                CreateUserAsync(new ApplicationUser
                {
                    FirstName = "Pero",
                    LastName = "Perić",
                    Email = "pero@peric.com",
                    UserName = "pero@peric.com",
                    DOB = DateTime.Now.AddYears(-50),
                    PhoneNumber = "+385991234567",
                    Address = new List<Address>
                    {
                        new Address
                        {
                        Country = "Hrvatska",
                        ZipCode = "34000",
                        City = "Požega",
                        Street = "Zagrebačka 21"
                        }
                    }

                }, "Pa$$word123", Roles.Admin).Wait();


            }
        }
        public async Task CreateRoleAsync(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role

                });
            }
        }

        public async Task CreateUserAsync(ApplicationUser user, string password, string role)
        {
            var find = await userManager.FindByEmailAsync(user.Email);
            if (find != null)
            {
                return;
            }
            user.EmailConfirmed = true;

            var createdUser = await userManager.CreateAsync(user, password);
            if (createdUser.Succeeded)
            {
                var userToRole = await userManager.AddToRoleAsync(user, role);
                if (!userToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }

        }
    }
}
