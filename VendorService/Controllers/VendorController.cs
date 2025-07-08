using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using VendorService.Models.Dtos;

namespace VendorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        public IDbConnection _db;
        public ILogger<VendorController> _logger;
        public VendorController(IDbConnection db, ILogger<VendorController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet("/GetAllVendors")]
        public async Task<IActionResult> Index() 
        {
            var sql = "Select * from VendorProfile";
            _logger.LogInformation("Executing SQL: {Sql}", sql);
            try
            {
                _logger.LogInformation("Trying to Retrive all vendors from the database.");

                var result = await _db.QueryAsync<VendorDto>(sql);
                if (result == null || !result.Any())
                {
                    _logger.LogWarning("No vendors found.");
                    return NotFound("No vendors found.");
                }

                _logger.LogInformation("Vendors retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while retrieving vendors.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
