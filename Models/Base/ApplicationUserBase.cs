using Microsoft.AspNetCore.Identity;

namespace TestSeminar.Models.Base
{
    public class ApplicationUserBase /*: IdentityUser*/
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
    }
}
