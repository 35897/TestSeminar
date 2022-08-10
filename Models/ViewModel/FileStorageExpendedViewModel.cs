using System.Net.Mime;
using TestSeminar.Models.Base;

namespace TestSeminar.Models.ViewModel
{
    public class FileStorageExpendedViewModel : FileStorageBase
    {
        public int Id { get; set; }
        public string Base64 { get; set; }
        public FileStream FileStream { get; set; }
        public ContentDisposition ContentDisposition { get; set; }
    }
}
