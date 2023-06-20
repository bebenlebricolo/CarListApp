using CarListApi.Models;
using CarListApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth;
using Google.Apis.CloudIdentity.v1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CarListApi.Controllers
{
    [ApiController, AllowAnonymous]
    //[Route("[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public class AuthenticationController: ControllerBase
    {
        //private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config; 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) 
            {
                return Unauthorized();
            }
            
            var credentialsValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!credentialsValid)
            {
                return Unauthorized();
            }

            // Create and populate the new access token using appsettings as the source of configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtSettings:Key")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roles = await userManager.GetRolesAsync(user);
            var claims = await userManager.GetClaimsAsync(user);

            var tokenClaims = new List<Claim>
            {
                new Claim( JwtRegisteredClaimNames.Sub, user.Id),
                new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim( JwtRegisteredClaimNames.Email, user.Email!),
            }.Union(claims)
            .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var MaxAllowedTokenDurationMinutes = Convert.ToUInt32(_config.GetValue<string>("JwtSettings:DurationInMinutes")!);

            // Clamp requested duration to maximum allowed uuration, to avoid token abuse.
            var requestedDuration = loginDto.TokenLifespanRequestMinutes;
            if ( requestedDuration > MaxAllowedTokenDurationMinutes)
            {
                requestedDuration = MaxAllowedTokenDurationMinutes;
            }

            var expirationDate = DateTime.UtcNow.AddMinutes(requestedDuration);

            var securityToken = new JwtSecurityToken(
                issuer: _config.GetValue<string>("JwtSettings:Issuer")!,
                audience: _config.GetValue<string>("JwtSettings:Audience")!,
                claims: tokenClaims,
                expires: expirationDate,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            var response = new AuthResponseDto
            {
                AccessToken = accessToken,
                Username = loginDto.UserName,
                UserId = user.Id,
                ValidFrom = DateTime.UtcNow.ToString(),
                ValidUntil = expirationDate.ToString()
            };

            return Ok(response);
        }
    }
}