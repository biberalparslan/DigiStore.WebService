using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrunController : ControllerBase
    {
        private readonly IUrunService _service;

        public UrunController(IUrunService service)
        {
            _service = service;
        }

        /// <summary>
        /// Belirtilen marka için ürünleri getirir.
        /// </summary>
        [HttpGet("marka/{markaId}/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunByMarkaDto>>> GetUrunByMarka(int markaId, int uyeId)
        {
            if (markaId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "MarkaId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "MarkaId"
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            var list = await _service.GetUrunByMarkaAsync(markaId, uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Belirtilen kategori için ürünleri getirir.
        /// </summary>
        [HttpGet("kategori/{kategoriId}/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunByKategoriDto>>> GetUrunByKategori(int kategoriId, int uyeId)
        {
            if (kategoriId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "KategoriId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "KategoriId"
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            var list = await _service.GetUrunByKategoriAsync(kategoriId, uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Arama sorgusuna göre ürünleri getirir.
        /// </summary>
        [HttpGet("arama/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunByAramaDto>>> GetUrunByArama([FromQuery] string query, int uyeId)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Arama sorgusu gereklidir.",
                    field = "query"
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            var list = await _service.GetUrunByAramaAsync(query, uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Belirtilen ana kategori için ürünleri getirir.
        /// </summary>
        [HttpGet("anakategori/{anaKategoriId}/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunByAnaKategoriDto>>> GetUrunByAnaKategori(int anaKategoriId, int uyeId)
        {
            if (anaKategoriId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "AnaKategoriId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "AnaKategoriId"
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr.",
                    field = "UyeId"
                });
            }

            var list = await _service.GetUrunByAnaKategoriAsync(anaKategoriId, uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Tüm ürünleri getirir. UyeId belirtilmezse veya 0'dan küçükse varsayýlan deðer kullanýlýr.
        /// </summary>
        [HttpGet("tum-urunler")]
        public async Task<ActionResult<IEnumerable<TumUrunlerDto>>> GetTumUrunler([FromQuery] int? uyeId)
        {
            var list = await _service.GetTumUrunlerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Çok satýlan ürünleri getirir.
        /// </summary>
        [HttpGet("coksatilanlar/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunCokSatilanDto>>> GetCokSatilanlar(int uyeId)
        {
            var list = await _service.GetCokSatilanlarAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Fýrsat ürünlerini getirir.
        /// </summary>
        [HttpGet("firsat/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunFirsatDto>>> GetFirsatUrunleri(int uyeId)
        {
            var list = await _service.GetFirsatUrunleriAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Ýlgili ürünleri getirir.
        /// </summary>
        [HttpGet("ilgili/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunIlgiliDto>>> GetIlgiliUrunler(int uyeId, [FromQuery] int markaId, [FromQuery] int kategoriId, [FromQuery] int urunId)
        {
            var list = await _service.GetIlgiliUrunlerAsync(uyeId, markaId, kategoriId, urunId);
            return Ok(list);
        }

        /// <summary>
        /// Ýndirimli ürünleri getirir.
        /// </summary>
        [HttpGet("indirimli/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunIndirimliDto>>> GetIndirimliUrunler(int uyeId)
        {
            var list = await _service.GetIndirimliUrunlerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Son eklenen ürünleri getirir.
        /// </summary>
        [HttpGet("son-eklenen/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunSonEklenenDto>>> GetSonEklenenler(int uyeId)
        {
            var list = await _service.GetSonEklenenlerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Son gezilen ürünleri getirir.
        /// </summary>
        [HttpGet("son-gezilen/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunSonGezilenDto>>> GetSonGezilenler(int uyeId)
        {
            var list = await _service.GetSonGezilenlerAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Barkoda göre garanti bilgilerini getirir.
        /// </summary>
        [HttpGet("garanti/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunGarantiDto>>> GetGaranti(int uyeId, [FromQuery] string barkod)
        {
            var list = await _service.GetGarantiAsync(uyeId, barkod);
            return Ok(list);
        }

        /// <summary>
        /// Arama çubuðu sonuçlarýný getirir.
        /// </summary>
        [HttpGet("searchbar/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<UrunSearchBarDto>>> GetUrunBySearchBar(int uyeId, [FromQuery] string query)
        {
            var list = await _service.GetUrunBySearchBarAsync(query, uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Ürün detaylarýný döner: 1) detay, 2) resimler, 3) özellikler.
        /// </summary>
        [HttpGet("detay/{urunId}/user/{uyeId}")]
        public async Task<ActionResult<object>> GetUrunDetay(int urunId, int uyeId)
        {
            var (detay, resimler, ozellikler) = await _service.GetUrunDetayAsync(urunId, uyeId);
            if (detay == null) return NotFound(new { error = "Bulunamadý", message = "Ürün bulunamadý." });
            return Ok(new { detay, resimler, ozellikler });
        }
    }
}
