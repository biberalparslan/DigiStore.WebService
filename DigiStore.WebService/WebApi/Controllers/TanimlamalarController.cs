using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using System.Collections.Generic;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TanimlamalarController : ControllerBase
    {
        private readonly ITanimlamalarService _service;
        private readonly IAnaKategoriService _anaKategoriService;
        private readonly IKategoriService _kategoriService;

        public TanimlamalarController(
            ITanimlamalarService service,
            IAnaKategoriService anaKategoriService,
            IKategoriService kategoriService)
        {
            _service = service;
            _anaKategoriService = anaKategoriService;
            _kategoriService = kategoriService;
        }

        /// <summary>
        /// Firma bilgilerini getirir.
        /// </summary>
        [HttpGet("firma-bilgileri")]
        public async Task<ActionResult<FirmaBilgileriDto>> GetFirmaBilgileri()
        {
            var dto = await _service.GetFirmaBilgileriAsync();
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Firma bilgileri bulunamadý." });
            return Ok(dto);
        }

        /// <summary>
        /// Þehir listesini getirir.
        /// </summary>
        [HttpGet("sehirler")]
        public async Task<ActionResult<IEnumerable<SehirlerDto>>> GetSehirler()
        {
            var list = await _service.GetSehirlerAsync();
            return Ok(list);
        }

        /// <summary>
        /// Seçilen þehre ait ilçe listesini getirir.
        /// </summary>
        [HttpGet("ilceler/{sehirId}")]
        public async Task<ActionResult<IEnumerable<IlcelerDto>>> GetIlceler(int sehirId)
        {
            var list = await _service.GetIlcelerAsync(sehirId);
            return Ok(list);
        }

        /// <summary>
        /// Döviz kuru bilgilerini getirir.
        /// </summary>
        [HttpGet("kur-bilgileri")]
        public async Task<ActionResult<IEnumerable<DovizKuruDto>>> GetKurBilgileri()
        {
            var list = await _service.GetKurBilgileriAsync();
            return Ok(list);
        }

        /// <summary>
        /// Marka listesini getirir (alfabetik sýralý).
        /// </summary>
        [HttpGet("markalar")]
        public async Task<ActionResult<IEnumerable<MarkalarDto>>> GetMarkalar()
        {
            var list = await _service.GetMarkalarAsync();
            return Ok(list);
        }

        /// <summary>
        /// Banner resimlerini getirir.
        /// </summary>
        [HttpGet("banner-resimler")]
        public async Task<ActionResult<IEnumerable<BannerResimDto>>> GetBannerResimler()
        {
            var list = await _service.GetBannerResimAsync();
            return Ok(list);
        }

        /// <summary>
        /// Vitrin (showcase) resimlerini getirir.
        /// </summary>
        [HttpGet("vitrin-resimler")]
        public async Task<ActionResult<IEnumerable<VitrinResimDto>>> GetVitrinResimler()
        {
            var list = await _service.GetVitrinResimAsync();
            return Ok(list);
        }

        /// <summary>
        /// Ana kategorileri getirir.
        /// </summary>
        [HttpGet("ana-kategoriler")]
        public async Task<ActionResult<IEnumerable<AnaKategoriDto>>> GetAnaKategoriler()
        {
            var list = await _anaKategoriService.GetAnaKategoriAsync();
            return Ok(list);
        }

        /// <summary>
        /// Belirtilen ana kategoriye ait kategorileri getirir.
        /// </summary>
        [HttpGet("kategoriler")]
        public async Task<ActionResult<IEnumerable<KategoriDto>>> GetKategoriler([FromQuery] string dil, [FromQuery] int anaKategoriId)
        {
            var list = await _kategoriService.GetKategoriAsync(dil, anaKategoriId);
            return Ok(list);
        }

        /// <summary>
        /// Popüler kategorileri getirir.
        /// </summary>
        [HttpGet("kategoriler-populer")]
        public async Task<ActionResult<IEnumerable<KategoriDto>>> GetPopulerKategoriler()
        {
            var list = await _kategoriService.GetPopulerKategoriAsync();
            return Ok(list);
        }

        /// <summary>
        /// Breadcrumb bilgisini getirir. Parametrelerden biri -1 olmalýdýr.
        /// </summary>
        [HttpGet("breadcrumb")]
        public async Task<ActionResult<List<Dictionary<string, object?>>>> GetBreadcrumb([FromQuery] int anaKategoriId = -1, [FromQuery] int kategoriId = -1)
        {
            var list = await _kategoriService.GetBreadcrumbAsync(anaKategoriId, kategoriId);
            return Ok(list);
        }
    }
}
