using TestSeminar.Models.Base;

namespace TestSeminar.Models.Dbo
{
    public class FileStorage : FileStorageBase, IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

    }
}
