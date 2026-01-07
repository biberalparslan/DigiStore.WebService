using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICariService _cariService;
        private readonly IAuthService _authService;

        public AuthController(ICariService cariService, IAuthService authService)
        {
            _cariService = cariService;
            _authService = authService;
        }

        /// <summary>
        /// Login endpoint - Returns JWT token
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var bayi = await _cariService.GetBayiDogrulamaAsync(
                request.BayiKodu, 
                request.Eposta, 
                request.Parola);

            if (bayi == null || !bayi.DogrulandiMi)
            {
                return Unauthorized(new { message = "Invalid credentials or account not verified" });
            }

            var token = _authService.GenerateJwtToken(bayi.UyeId, bayi.Eposta!);

            return Ok(new
            {
                token,
                uyeId = bayi.UyeId,
                email = bayi.Eposta,
                adSoyad = bayi.AdSoyad,
                expiresIn = 3600 // seconds
            });
        }
    }

    public class LoginRequest
    {
        public required string BayiKodu { get; set; }
        public required string Eposta { get; set; }
        public required string Parola { get; set; }
    }
}