using App.Application.DTOs;
using System.Security.Claims;

namespace App.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal?> LoginAsync(LoginRequest request);
        Task<bool> RegisterUserAsync(RegisterUserDto model);
    }
}
