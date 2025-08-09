using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? VendorID { get; set; }             // If user is a vendor
        public int? BusinessUserID { get; set; }       // If user is a business user
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
