using System.ComponentModel.DataAnnotations;
using TestSeminar.Models.Base;

namespace TestSeminar.Models.Dbo
{
    public class Address : AddressBase, IEntityBase
    {

        [Key]
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Created { get; set; }
    }
}
