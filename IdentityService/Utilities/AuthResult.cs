using IdentityService.Models.Dtos;

namespace IdentityService.Utilities
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
        public string Jwt { get; set; }
        public int ExpiresInMinutes { get; } = 60;

        public UserDto UserDto { get; set; }

        public static AuthResult Success() => new AuthResult { Succeeded = true };
        public static AuthResult Success(string token, UserDto userDto) => new AuthResult { Succeeded = true, Jwt = token, UserDto = userDto};
        public static AuthResult Fail(string message) => new AuthResult { Succeeded = false, ErrorMessage = message };
    }
}
