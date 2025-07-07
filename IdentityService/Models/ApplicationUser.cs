using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ShopID { get; set; }  // Optional: link to RetailShop
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
