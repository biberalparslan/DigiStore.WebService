using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SepetController : ControllerBase
    {
        private readonly ISepetService _service;
        public SepetController(ISepetService service)
        {
            _service = service;
        }

        /// <summary>
        /// Belirtilen kullanýcýnýn sepetini getirir.
        /// </summary>
        [HttpGet("{uyeId}")]
        public async Task<ActionResult<IEnumerable<SepetDto>>> GetSepetim(int uyeId)
        {
            var list = await _service.GetSepetimAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Sepete yeni bir madde ekler.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertSepet([FromBody] InsertSepetRequest request)
        {
            // Validation
            if (request.UyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            if (request.UrunId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UrunId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UrunId"
                });
            }

            if (request.Adet <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Adet gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "Adet"
                });
            }

            var ok = await _service.InsertSepetAsync(request.UyeId, request.UrunId, request.Adet);
            
            if (!ok)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    error = "EklemeBasarisiz", 
                    message = "Sepet ögesi eklenemedi." 
                });
            }

            return Ok(new 
            { 
                success = true, 
                message = "Ürün sepete baþarýyla eklendi." 
            });
        }

        /// <summary>
        /// Sepetteki bir ürünün adetini günceller.
        /// </summary>
        [HttpPut("quantity")]
        public async Task<IActionResult> UpdateSepetAdet([FromBody] UpdateSepetQuantityRequest request)
        {
            // Validation
            if (request.UyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            if (request.UrunId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UrunId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UrunId"
                });
            }

            if (request.Adet <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Adet gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "Adet"
                });
            }

            var ok = await _service.UpdateSepetAdetAsync(request.UyeId, request.UrunId, request.Adet);
            
            if (!ok)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    error = "GuncellemeBasarisiz", 
                    message = "Sepet adedi güncellenemedi." 
                });
            }

            return Ok(new 
            { 
                success = true, 
                message = "Sepet adedi baþarýyla güncellendi." 
            });
        }

        /// <summary>
        /// Sepetten bir maddeyi siler.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteSepet([FromBody] DeleteSepetRequest request)
        {
            // Validation
            if (request.UyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            if (request.UrunId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UrunId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UrunId"
                });
            }

            var ok = await _service.DeleteSepetAsync(request.UyeId, request.UrunId);
            
            if (!ok)
            {
                return NotFound(new 
                { 
                    success = false, 
                    error = "Bulunamadý", 
                    message = "Sepet ögesi silinemedi veya bulunamadý." 
                });
            }

            return Ok(new 
            { 
                success = true, 
                message = "Ürün sepetten baþarýyla silindi." 
            });
        }

        /// <summary>
        /// Sepeti sipariþe dönüþtürür. -1 = teslimat adresi yok, pozitif sayý = sepet adedi.
        /// </summary>
        [HttpPost("siparis/{uyeId}")]
        public async Task<ActionResult<object>> CreateSiparis(int uyeId)
        {
            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    code = 0
                });
            }

            var result = await _service.InsertSiparisAsync(uyeId);
            
            return result switch
            {
                -1 => BadRequest(new 
                { 
                    success = false, 
                    message = "Teslimat adresi belirtilmemiþ. Lütfen önce teslimat adresinizi ekleyin.",
                    code = -1 
                }),
                0 => Ok(new 
                { 
                    success = true, 
                    message = "Sipariþ baþarýyla oluþturuldu.",
                    sepetAdedi = result,
                    code = result
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

    /// <summary>
    /// Sepete ürün ekleme isteði
    /// </summary>
    public class InsertSepetRequest
    {
        /// <summary>
        /// Üye ID
        /// </summary>
        public int UyeId { get; set; }
        
        /// <summary>
        /// Ürün ID
        /// </summary>
        public int UrunId { get; set; }
        
        /// <summary>
        /// Ürün adedi
        /// </summary>
        public int Adet { get; set; }
    }

    /// <summary>
    /// Sepet ürün adeti güncelleme isteði
    /// </summary>
    public class UpdateSepetQuantityRequest
    {
        /// <summary>
        /// Üye ID
        /// </summary>
        public int UyeId { get; set; }
        
        /// <summary>
        /// Ürün ID
        /// </summary>
        public int UrunId { get; set; }
        
        /// <summary>
        /// Yeni ürün adedi
        /// </summary>
        public int Adet { get; set; }
    }

    /// <summary>
    /// Sepetten ürün silme isteði
    /// </summary>
    public class DeleteSepetRequest
    {
        /// <summary>
        /// Üye ID
        /// </summary>
        public int UyeId { get; set; }
        
        /// <summary>
        /// Ürün ID
        /// </summary>
        public int UrunId { get; set; }
    }
}
