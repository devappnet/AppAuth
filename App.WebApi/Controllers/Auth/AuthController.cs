using App.Application.DTOs;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using App.WebApi.Configuration;

namespace App.WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : AppBaseController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest login)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal principal = await authService.LoginAsync(login);
                if (principal != null)
                {
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(principal), authProperties);
                    return Ok(new ResponseBase<List<Claim>>
                    {
                        StatusCode = 200,
                        Message = "Success"
                    });
                }
                else
                    return Unauthorized();
            }
            else
            {
                var errors = ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage);
                return BadRequest(new ResponseBase<object>
                {
                    StatusCode = 400,
                    Message = "Request Failed",
                    Errors = errors
                });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> ResgisterAsync([FromBody] RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.RegisterUserAsync(model);
                if (result)
                    return Ok(new { Message = "User registered successfully!" });

                return BadRequest(new { Message = "Registration failed!" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage);
                return BadRequest(new ResponseBase<object>
                {
                    StatusCode = 400,
                    Message = $"Request {nameof(model)} Failed",
                    Errors = errors
                });
            }
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new ResponseBase<object>
            {
                StatusCode = 200,
                Message = "Logout success"
            });
        }
        [HttpGet("user")]
        [Authorize]
        [Permission(PermissionConstants.Auth.View)]
        public IActionResult GetUser()
        {
            return Ok(new ResponseBase<string>
            {
                StatusCode = 200,
                Data = User.Identity?.Name
            });
        }
    }
}
