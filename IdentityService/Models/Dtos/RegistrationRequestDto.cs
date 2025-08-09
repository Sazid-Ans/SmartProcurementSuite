using System.ComponentModel.DataAnnotations;

namespace IdentityService.Models.Dtos
{
    public class RegistrationRequestDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Vendor" or "BusinessUser"
        public int? WarehouseID { get; set; } // required for BusinessUser
    }
}
