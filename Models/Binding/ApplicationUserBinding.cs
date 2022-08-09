using TestSeminar.Models.Base;

namespace TestSeminar.Models.Binding
{
    public class ApplicationUserBinding : ApplicationUserBase
    {
        
        public AddressBinding UserAddress { get; set; }

    }
}
