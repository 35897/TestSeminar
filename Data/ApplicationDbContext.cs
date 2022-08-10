using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;
using TestSeminar.Models.Binding;

namespace TestSeminar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Product> Product { get; set; }               
        public DbSet<Address> Address { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<TestSeminar.Models.ViewModel.ProductViewModel>? ProductViewModel { get; set; }
        
        public DbSet<FileStorage> FileStorage { get; set; } 
       
    }
}