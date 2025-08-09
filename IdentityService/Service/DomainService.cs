using System.Data;
using Dapper;
namespace IdentityService.DomainService
{

    public class DomainService : IDomainService
    {
        private readonly IDbConnection _db;

        public DomainService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> IsVendorEmailExists(object email)
        {
            var sql = "SELECT COUNT(1) FROM VendorProfile WHERE ContactEmail = @Email";
            var count = await _db.ExecuteScalarAsync<int>(sql, new { Email = email?.ToString() });
            return count > 0;
        }

        public async Task<bool> IsBusinessUserEmailExists(object email)
        {
            var sql = "SELECT COUNT(1) FROM BusinessUser WHERE Email = @Email";
            var count = await _db.ExecuteScalarAsync<int>(sql, new { Email = email?.ToString() });
            return count > 0;
        }

        public async Task<int?> CreateVendorProfileAsync(object fullName, object email)
        {
            var sql = @"
            INSERT INTO VendorProfile (CompanyName, ContactEmail)
            VALUES (@CompanyName, @Email);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await _db.ExecuteScalarAsync<int?>(sql, new
            {
                CompanyName = fullName?.ToString(),
                Email = email?.ToString()
            });
        }

        public async Task<int?> CreateBusinessUserAsync(string fullName, string email, int warehouseId)
        {
            var sql = @"
            INSERT INTO BusinessUser (FullName, Email, WarehouseID, Role, IsActive)
            VALUES (@FullName, @Email, @WarehouseID, 'Registered', 1);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await _db.ExecuteScalarAsync<int?>(sql, new
            {
                FullName = fullName,
                Email = email,
                WarehouseID = warehouseId
            });
        }
    }

}
