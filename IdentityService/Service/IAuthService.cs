using IdentityService.Models;
using IdentityService.Models.Dtos;
using IdentityService.Utilities;

namespace IdentityService.Service
{
    public interface IAuthService
    {
        Task<AuthResult> LoginUserAsync(LoginRequestDto user);
        Task<AuthResult> RegisterUserAsync(RegistrationRequestDto dto);
    }
}
