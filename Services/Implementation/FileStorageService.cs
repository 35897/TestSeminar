using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using TestSeminar.Data;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;
using TestSeminar.Services.Interface;

namespace TestSeminar.Services.Implementation
{
    public class FileStorageService : IFileStorageService
    {
        public readonly ApplicationDbContext db;
        private IWebHostEnvironment env;
        private IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public FileStorageService(ApplicationDbContext db, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.db = db;
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<FileStorageViewModel> AddFileToStorage(IFormFile file)
        {

            var dbo = new FileStorage();
            db.FileStorage.Add(dbo);
            await db.SaveChangesAsync();
            var savedFile = await AddToLocalStorage(file, dbo.Id);

            mapper.Map(savedFile, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<FileStorageViewModel>(dbo);
        }
        private async Task<FileStorageViewModel> AddToLocalStorage(IFormFile file, long fileuploadId)
        {
            if (file == null)
            {
                return null;
            }
            string folderPath = env.ContentRootPath + @"\upload\" + fileuploadId;
            Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, file.FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}";

            var response = new FileStorageViewModel
            {
                DownloadUrl = url + "/shared/files/" + fileuploadId,
                PhysicalPath = filePath,
                FileName = file.FileName,
                FileExtension = Path.GetExtension(file.FileName)
            };
            return response;
        }
        public async Task<FileStorageExpendedViewModel> GetFile(long id)
        {
            try
            {
                var dbo = await db.FileStorage.FirstOrDefaultAsync(x => x.Id == id);
                var response = mapper.Map<FileStorageExpendedViewModel>(dbo);
                if (dbo != null)
                {
                    response.ContentDisposition = new ContentDisposition
                    {
                        FileName = dbo.FileName,
                        Inline = false,
                    };
                    response.FileStream = File.OpenRead(dbo.PhysicalPath);
                    response.Base64 = GetBase64FromPhysicalPath(dbo.PhysicalPath);

                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        private string GetBase64FromPhysicalPath(string path)
        {
            var file = File.ReadAllBytes(path);
            return Convert.ToBase64String(file);
        }
    }
}
