using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestSeminar.Data;
using TestSeminar.Models.Binding;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;
using TestSeminar.Services.Interface;

namespace TestSeminar.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IMapper mapper;
        private readonly ApplicationDbContext db;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.db = db;
        }

        public async Task<ApplicationUserViewModel> GetUserAsync(string id)
        {
            var dboUser = await db.Users
                .Include(x => x.Address)                
                .FirstOrDefaultAsync(x => x.Id == id);
            if(dboUser == null)
            {
                return null;
            }

            var response = mapper.Map<ApplicationUserViewModel>(dboUser);
            response.Role = await GetUserRoleAsync(dboUser.Email);
            return response;
        }

        public async Task<List<ApplicationUserViewModel>> GetUsersAsync()
        {
            var users = await db.Users
                 .Include(x => x.Address)                 
                 .ToListAsync();   
            
            var response = users.Select(x => mapper.Map<ApplicationUserViewModel>(x)).ToList();
            response.ForEach(x => x.Role = GetUserRoleAsync(x.Email).Result);
            return response;
        }


            public async Task<ApplicationUser> CreateUserAsync(ApplicationUserBinding model, string role)
        {
            var find = await userManager.FindByEmailAsync(model.Email);
            if (find != null)
            {
                return null;
            }

            var user = mapper.Map<ApplicationUser>(model);
            var address = mapper.Map<Address>(model.UserAddress);
            user.Address = new List<Address>() { address };

            user.EmailConfirmed = true;            

            var createdUser = await userManager.CreateAsync(user, model.Password);
            if (createdUser.Succeeded)
            {
                var userAddedToRole = await userManager.AddToRoleAsync(user, role);
                if (!userAddedToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }
            return user;
        }

        public async Task<ApplicationUserViewModel> UpdateUserAsync(ApplicationUserUpdateBinding model)
        {   
            var dbo =await db.Users
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(x=>x.Id == model.Id);

            var role = await db.Roles.FindAsync(model.RoleId);            
            if (role == null || dbo == null)
            {
                return null;
            }
            //var address = mapper.Map<Address>(model.UserAddress);
            //dbo.Address = new List<Address>() { address };

            await DeleteAllUserRoles(dbo);
            await userManager.AddToRoleAsync(dbo, role.Name);
            mapper.Map(model, dbo);

            
            await db.SaveChangesAsync();
            return mapper.Map<ApplicationUserViewModel>(dbo);
        }
        public async Task DeleteUserAsync(string id)
        {
            var dbo = await db.Users
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);            
            if (dbo != null)
            {
                db.Remove(dbo);
            }
            await db.SaveChangesAsync();
        }

        public async Task<string> GetUserRoleAsync(string email)
        {            
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }
            var role = await userManager.GetRolesAsync(user);             
            return role[0];
        }

        public async Task<List<UserRolesViewModel>> GetUserRoles()
        {
            var roles =await db.Roles.ToListAsync();
            if (!roles.Any())
            {
                return new List<UserRolesViewModel>();
            }
            return roles.Select(x => mapper.Map<UserRolesViewModel>(x)).ToList();
        }

        public async Task<ApplicationUserViewModel> AddUserAsync(ApplicationUserCreateBinding model)
        {
            var find = await userManager.FindByEmailAsync(model.Email);
            if (find != null)
            {
                return null;
            }

            var user = mapper.Map<ApplicationUser>(model);
            var address = mapper.Map<Address>(model.UserAddress);            
            user.Address = new List<Address>() { address };
            var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == model.RoleId);

            user.EmailConfirmed = true;

            var createdUser = await userManager.CreateAsync(user, model.Password);
            if (createdUser.Succeeded)
            {
                var userAddedToRole = await userManager.AddToRoleAsync(user, role.Name);
                if (!userAddedToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }
            return mapper.Map<ApplicationUserViewModel>(user);
        }

        private async Task DeleteAllUserRoles(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var item in userRoles)
            {
                await userManager.RemoveFromRoleAsync(user, item);
            }



        }
    }
}
