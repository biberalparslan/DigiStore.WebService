using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdresController : ControllerBase
    {
        private readonly IAdresService _service;

        public AdresController(IAdresService service)
        {
            _service = service;
        }

        /// <summary>
        /// Belirtilen kullanýcýnýn adreslerini getirir.
        /// </summary>
        [HttpGet("user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<AdresDto>>> GetAdresler(int uyeId)
        {
            var list = await _service.GetAdreslerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Belirtilen kullanýcý için ID'ye göre tek bir adres getirir.
        /// </summary>
        [HttpGet("{adresId}/user/{uyeId}")]
        public async Task<ActionResult<AdresDto>> GetAdresById(int adresId, int uyeId)
        {
            var dto = await _service.GetAdresByIdAsync(adresId, uyeId);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = $"ID {adresId} için adres bulunamadý (kullanýcý {uyeId})." });
            return Ok(dto);
        }

        /// <summary>
        /// Varsayýlan adresi belirler.
        /// </summary>
        [HttpPost("{adresId}/user/{uyeId}/default")]
        public async Task<IActionResult> SetDefaultAddress(int adresId, int uyeId)
        {
            var ok = await _service.UpdDefAdresAsync(adresId, uyeId);
            if (!ok) return NotFound(new { error = "Bulunamadý", message = "Adres bulunamadý veya varsayýlan olarak ayarlanamadý." });
            return Ok();
        }

        /// <summary>
        /// Adres ekler veya günceller. `AdresId = -1` ise yeni kayýt eklenir (dönen deðer 1); aksi halde güncellenir (dönen deðer 2).
        /// </summary>
        [HttpPost("upsert")]
        public async Task<ActionResult<int>> UpsertAddress([FromBody] AdresDto dto)
        {
            var result = await _service.UpdAdresAsync(dto);
            if (result == -1) return BadRequest(new { error = "Basarisiz", message = "Adres iþlemi baþarýsýz oldu." });
            return Ok(result);
        }

        /// <summary>
        /// Belirtilen adresi siler.
        /// </summary>
        [HttpDelete("{adresId}/user/{uyeId}")]
        public async Task<IActionResult> DeleteAdres(int adresId, int uyeId)
        {
            var ok = await _service.DelAdresAsync(adresId, uyeId);
            if (!ok) return NotFound(new { error = "Bulunamadý", message = $"ID {adresId} için adres silinemedi veya bulunamadý." });
            return NoContent();
        }
    }
}
