using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TestSeminar.Models.Binding;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;

namespace TestSeminar.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBinding, Product>();
            CreateMap<Product, ProductViewModel>();


            CreateMap<ProductViewModel, ProductUpdateBinding>();
            CreateMap<ProductUpdateBinding, Product>();



            //mape za kategoriju kasnije
            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();


            //mape za adresu kod prikaza korisnika
            CreateMap<AddressBinding, Address>();
            CreateMap<Address, AddressViewModel>();

            CreateMap<ApplicationUserBinding, ApplicationUser>().ForMember(x=>x.UserName, y => y.MapFrom(src=>src.Email));
            CreateMap<ApplicationUser, ApplicationUserViewModel>();


            CreateMap<ApplicationUserViewModel, ApplicationUserUpdateBinding>();
            CreateMap<ApplicationUserUpdateBinding, ApplicationUser>();

            CreateMap<ApplicationUserViewModel, ApplicationUserCreateBinding>();
            CreateMap<ApplicationUserCreateBinding, ApplicationUser>().ForMember(x => x.UserName, y => y.MapFrom(src => src.Email));

            //roles
            CreateMap<IdentityRole, UserRolesViewModel>();

            //slike
            CreateMap<FileStorage, FileStorageViewModel>();
            CreateMap<FileStorage, FileStorageExpendedViewModel>();

            CreateMap<FileStorageViewModel, FileStorage>().ForMember(x => x.Id, y => y.Ignore());

        }


    }
}
