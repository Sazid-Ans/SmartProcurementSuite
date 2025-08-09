
namespace IdentityService.DomainService
{
    public interface IDomainService
    {
        Task<int?> CreateBusinessUserAsync(string fullName, string email, int value);
        Task<int?> CreateVendorProfileAsync(object fullName, object email);
        Task<bool> IsBusinessUserEmailExists(object email);
        Task<bool> IsVendorEmailExists(object email);
    }
}
