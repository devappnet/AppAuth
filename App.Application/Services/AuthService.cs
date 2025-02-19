using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entity;
using App.Infrastructure;
using App.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.Application.Services
{
    public class AuthService(IRepositoryBase repository, IPasswordHasher<AspNetUser> _passwordHasher) : IAuthService
    {
        public async Task<ClaimsPrincipal?> LoginAsync(LoginRequest request)
        {
            ClaimsPrincipal principal = null;
            var user = await repository.Queryrable<AspNetUser>().AsNoTracking().Where(s => s.UserName == request.Username).FirstOrDefaultAsync();
            if (user is not null)
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.Username),
                        new Claim("Permission",request.Username == "dev3"?"0x1":"0x0")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    principal = new ClaimsPrincipal(identity);
                }
            }
            return principal;
        }
        public async Task<bool> RegisterUserAsync(RegisterUserDto model)
        {
            var checkmail = await repository.Queryrable<AspNetUser>().AnyAsync(s=>s.Email == model.Email);
            if (checkmail) throw new Exception(ConstantaHelper.Messsage.Email_Exists);

            var user = new AspNetUser
            {
                UserName = model.UserName,
                NormalizedUserName = model.UserName.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = string.Empty, // Will be hashed below
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
                Active = true
            };

            // Hash the password
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            await repository.AddAsync<AspNetUser>(user);
            await repository.SaveChangeAsync();

            return true;
        }
    }
}
