using Microsoft.AspNetCore.Identity;
using TestSeminar.Models.Binding;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;

namespace TestSeminar.Services.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUserBinding model, string role);
        Task<ApplicationUserViewModel> GetUserAsync(string id);
        Task<List<ApplicationUserViewModel>> GetUsersAsync();
        Task<ApplicationUserViewModel> UpdateUserAsync(ApplicationUserUpdateBinding model);
        Task DeleteUserAsync(string id);
        Task<string> GetUserRoleAsync(string id);
        Task<List<UserRolesViewModel>> GetUserRoles();
        Task<ApplicationUserViewModel> AddUserAsync(ApplicationUserCreateBinding model);
    }
}