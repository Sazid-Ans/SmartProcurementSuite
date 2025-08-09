namespace IdentityService.Utilities
{
    public class DomainUserResult
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
        public int? VendorID { get; set; }
        public int? BusinessUserID { get; set; }

        public static DomainUserResult Success(int? vendorId, int? businessUserId) =>
            new() { Succeeded = true, VendorID = vendorId, BusinessUserID = businessUserId };

        public static DomainUserResult Fail(string message) =>
            new() { Succeeded = false, ErrorMessage = message };
    }

}
