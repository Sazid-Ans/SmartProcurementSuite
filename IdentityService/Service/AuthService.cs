using IdentityService.DomainService;
using IdentityService.Models;
using IdentityService.Models.Dtos;
using IdentityService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDomainService _domainService;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IDomainService vendorDomainService, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _domainService = vendorDomainService;
            _config = config;
        }

        public async Task<AuthResult> LoginUserAsync(LoginRequestDto user)
        {
            var ApplicationUser = await _userManager.FindByEmailAsync(user?.UserName);
            var role = await _userManager.GetRolesAsync(ApplicationUser);
            if (ApplicationUser == null)
                return AuthResult.Fail("user not found");

            var checkPassword = await _userManager.CheckPasswordAsync(ApplicationUser, user.password);
            if (!checkPassword)
                return AuthResult.Fail("Incorrect password");
            var token = GenerateJwtToken(ApplicationUser, role);
            var userInfo = new UserDto
            {
                UserEmail = ApplicationUser.Email,
                UserName = ApplicationUser.UserName,
                UserPhoneNumber = ApplicationUser.UserName
            };
            return AuthResult.Success(token,userInfo);
        }

        public async Task<AuthResult> RegisterUserAsync(RegistrationRequestDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return AuthResult.Fail("User already exists");

            var domainIds = await CreateDomainUserAsync(dto);
            if (!domainIds.Succeeded)
                return AuthResult.Fail(domainIds.ErrorMessage);

            var identityResult = await CreateIdentityUserAsync(dto, domainIds);
            if (!identityResult.Succeeded)
                return identityResult;

            return AuthResult.Success();
        }

        private async Task<DomainUserResult> CreateDomainUserAsync(RegistrationRequestDto dto)
        {
            int? vendorId = null;
            int? businessUserId = null;

            switch (dto.Role.ToLower())
            {
                case "vendor":
                    if (await _domainService.IsVendorEmailExists(dto.Email))
                        return DomainUserResult.Fail("Vendor already exists");
                    vendorId = await _domainService.CreateVendorProfileAsync(dto.FullName, dto.Email);
                    break;

                case "businessuser":
                    if (dto.WarehouseID == null)
                        return DomainUserResult.Fail("WarehouseID required");
                    if (await _domainService.IsBusinessUserEmailExists(dto.Email))
                        return DomainUserResult.Fail("Business user already exists");
                    businessUserId = await _domainService.CreateBusinessUserAsync(dto.FullName, dto.Email, dto.WarehouseID.Value);
                    break;

                default:
                    return DomainUserResult.Fail("Invalid role");
            }

            return DomainUserResult.Success(vendorId, businessUserId);
        }

        private async Task<AuthResult> CreateIdentityUserAsync(RegistrationRequestDto dto, DomainUserResult domain)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                VendorID = domain.VendorID,
                BusinessUserID = domain.BusinessUserID,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return AuthResult.Fail("Identity creation failed");

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));

            await _userManager.AddToRoleAsync(user, dto.Role);

            return AuthResult.Success();
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("FullName", user.FullName ?? ""),
        new Claim("VendorID", user.VendorID?.ToString() ?? ""),
        new Claim("BusinessUserID", user.BusinessUserID?.ToString() ?? "")
    };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
