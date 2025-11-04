using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeklifController : ControllerBase
    {
        private readonly ITeklifService _service;

        public TeklifController(ITeklifService service)
        {
            _service = service;
        }

        /// <summary>
        /// Bir kullanýcýnýn tekliflerini getirir.
        /// </summary>
        [HttpGet("user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<TeklifDto>>> GetTeklifler(int uyeId)
        {
            var list = await _service.GetTekliflerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Teklifin detaylarýný getirir.
        /// </summary>
        [HttpGet("{teklifId}/user/{uyeId}")]
        public async Task<ActionResult<TeklifDto>> GetTeklifDetay(int teklifId, int uyeId)
        {
            var dto = await _service.GetTeklifDetayAsync(teklifId, uyeId);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Teklif bulunamadý." });
            return Ok(dto);
        }

        /// <summary>
        /// Teklif detayýný HTML formatýnda getirir.
        /// </summary>
        [HttpGet("{teklifId}/user/{uyeId}/html")]
        public async Task<ActionResult<string>> GetTeklifDetayHtml(int teklifId, int uyeId)
        {
            var html = await _service.GetTeklifDetayHtmlAsync(teklifId, uyeId);
            if (html == null) return NotFound(new { error = "Bulunamadý", message = "Teklif HTML bulunamadý." });
            return Content(html, "text/html");
        }

        /// <summary>
        /// Teklifin meta verilerini günceller.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateTeklif([FromBody] TeklifDto dto)
        {
            var ok = await _service.UpdateTeklifAsync(dto);
            if (!ok) return BadRequest(new { error = "GuncellemeBasarisiz", message = "Teklif güncellenemedi." });
            return Ok();
        }

        /// <summary>
        /// Teklif satýrlarýnýn fiyatlarýný günceller.
        /// </summary>
        [HttpPut("price")]
        public async Task<IActionResult> UpdateTeklifFiyat([FromBody] TeklifDto dto)
        {
            if (dto.UyeId == null || dto.TeklifId == 0) return BadRequest(new { error = "GecersizGirdi", message = "UyeId ve TeklifId gerekli." });
            if (dto.Details == null || dto.Details.Count == 0) return BadRequest(new { error = "GecersizGirdi", message = "Detaylarda UrunId ve BirimFiyat gereklidir." });

            // Iterate details and update each price
            foreach (var item in dto.Details)
            {
                if (item.UrunId == 0 || item.BirimFiyat == null) continue;
                var ok = await _service.UpdateTeklifFiyatAsync(dto.UyeId.Value, dto.TeklifId, item.UrunId, item.BirimFiyat.Value);
                if (!ok) return BadRequest(new { error = "GuncellemeBasarisiz", message = $"UrunId {item.UrunId} için fiyat güncellenemedi." });
            }

            return Ok();
        }

        /// <summary>
        /// Bir teklifi siler.
        /// </summary>
        [HttpDelete("{teklifId}/user/{uyeId}")]
        public async Task<IActionResult> DeleteTeklif(int teklifId, int uyeId)
        {
            var ok = await _service.DeleteTeklifAsync(teklifId, uyeId);
            if (!ok) return NotFound(new { error = "Bulunamadý", message = "Teklif silinemedi veya bulunamadý." });
            return NoContent();
        }

        /// <summary>
        /// Tekliften sipariþ oluþturur. -1 = teslimat adresi yok, -2 = yetersiz stok, -3 = baþarýlý.
        /// </summary>
        [HttpPost("{teklifId}/siparis")]
        public async Task<ActionResult<object>> CreateSiparisFromTeklif(
            int teklifId, 
            [FromBody] TeklifSiparisRequest request)
        {
            if (request.UyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir.",
                    code = 0
                });
            }

            var result = await _service.InsertSiparisFromTeklifAsync(
                teklifId, 
                request.UyeId, 
                request.TeslimatAdresId);
            
            return result switch
            {
                -1 => BadRequest(new 
                { 
                    success = false, 
                    message = "Teslimat adresi belirtilmemiþ. Lütfen önce teslimat adresinizi ekleyin.",
                    code = -1 
                }),
                -2 => BadRequest(new 
                { 
                    success = false, 
                    message = "Bazý ürünlerin stoðu yetersiz. Lütfen teklifi kontrol edin.",
                    code = -2 
                }),
                -3 => Ok(new 
                { 
                    success = true, 
                    message = "Sipariþ baþarýyla oluþturuldu.",
                    code = -3
                }),
                _ => StatusCode(500, new 
                { 
                    success = false, 
                    message = "Sipariþ oluþturulamadý.",
                    code = 0
                })
            };
        }
    }

    public class TeklifSiparisRequest
    {
        public int UyeId { get; set; }
        public int TeslimatAdresId { get; set; } = -1;
    }
}
