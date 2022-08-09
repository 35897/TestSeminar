using TestSeminar.Models.Base;

namespace TestSeminar.Models.ViewModel
{
    public class ApplicationUserViewModel : ApplicationUserBase
    {
        public string Id { get; set; }
        public List<AddressViewModel> Address { get; set; }

        public string Role { get; set; }    
    }
}
