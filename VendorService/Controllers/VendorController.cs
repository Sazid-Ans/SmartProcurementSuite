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
        public VendorController(IDbConnection db)
        {
            _db = db;
        }
    }
}
