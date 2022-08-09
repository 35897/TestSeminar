namespace TestSeminar.Models.Dbo
{
    public interface IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
