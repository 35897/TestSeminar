using Microsoft.AspNetCore.Mvc;
using TestSeminar.Services.Interface;

namespace TestSeminar.Controllers
{
    [Route("[controller]")]
    public class SharedController : Controller
    {
        public readonly IFileStorageService fileStorageService;

        public SharedController(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }
        [Route("files/{id}")]
        public async Task<IActionResult> GetFile(int id )
        {
            var img = await fileStorageService.GetFile(id);
            if (img == null)
            {
                return NoContent();
            }
            Response.Headers.Add("Content-Disposition", img.ContentDisposition.ToString());

            return File(img.FileStream, "image/" + img.FileExtension.Replace(".", string.Empty));
        }
    }
}
