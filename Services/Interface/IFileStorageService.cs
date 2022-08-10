using TestSeminar.Models.ViewModel;

namespace TestSeminar.Services.Interface
{
    public interface IFileStorageService
    {
        Task<FileStorageViewModel> AddFileToStorage(IFormFile file);
        Task<FileStorageExpendedViewModel> GetFile(long id);
    }
}