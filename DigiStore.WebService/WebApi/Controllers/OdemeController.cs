using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigiStore.WebService.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OdemeController : ControllerBase
    {
        private readonly IOdemeService _service;

        public OdemeController(IOdemeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Ödeme kaydý oluþturur ve ödeme bilgilerini döndürür.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OdemeInsertDto>> CreateOdeme([FromBody] OdemeCreateRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.UyeId <= 0) missingFields.Add("UyeId");
            if (request.OdenecekTutar <= 0) missingFields.Add("OdenecekTutar");
            if (request.KrediKarti <= 0) missingFields.Add("KrediKarti");
            if (request.Taksit <= 0) missingFields.Add("Taksit");
            if (string.IsNullOrWhiteSpace(request.KartSahibi)) missingFields.Add("KartSahibi");
            if (string.IsNullOrWhiteSpace(request.KartNo)) missingFields.Add("KartNo");
            if (string.IsNullOrWhiteSpace(request.Ip)) missingFields.Add("Ip");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            var result = await _service.InsertOdemeAsync(
                request.UyeId,
                request.OdenecekTutar,
                request.KrediKarti,
                request.Taksit,
                request.KartSahibi,
                request.KartNo,
                request.Ip);

            if (result == null)
            {
                return StatusCode(500, new 
                { 
                    success = false, 
                    message = "Ödeme kaydý oluþturulamadý." 
                });
            }

            return Ok(new 
            { 
                success = true, 
                message = "Ödeme kaydý baþarýyla oluþturuldu.",
                data = result
            });
        }

        /// <summary>
        /// Taksit seçeneklerini getirir.
        /// </summary>
        [HttpGet("taksit-secenekleri")]
        public async Task<ActionResult<IEnumerable<TaksitSecenekDto>>> GetTaksitSecenekleri(
            [FromQuery] int uyeId, 
            [FromQuery] int posNo)
        {
            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            if (posNo <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "PosNo gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            var list = await _service.GetTaksitSecenekleriAsync(uyeId, posNo);
            return Ok(list);
        }

        /// <summary>
        /// Kart BIN koduna göre ödeme seçeneklerini getirir.
        /// </summary>
        [HttpGet("odeme-secenekleri")]
        public async Task<ActionResult<IEnumerable<OdemeSecenekDto>>> GetOdemeSecenekleri(
            [FromQuery] int binKodu,
            [FromQuery] int uyeId, 
            [FromQuery] int posNo)
        {
            if (binKodu <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "BinKodu gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            if (posNo <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "PosNo gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            var list = await _service.GetOdemeSecenekleriAsync(binKodu, uyeId, posNo);
            return Ok(list);
        }

        /// <summary>
        /// Ödeme dekontu (makbuzu) bilgilerini getirir.
        /// </summary>
        [HttpGet("dekont")]
        public async Task<ActionResult<DekontDto>> GetDekontBilgileri(
            [FromQuery] int odemeId,
            [FromQuery] int uyeId,
            [FromQuery] string odemeKey)
        {
            if (odemeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "OdemeId gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            if (uyeId <= 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "UyeId gereklidir ve 0'dan büyük olmalýdýr." 
                });
            }

            if (string.IsNullOrWhiteSpace(odemeKey))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "OdemeKey gereklidir." 
                });
            }

            var dekont = await _service.GetDekontBilgileriAsync(odemeId, uyeId, odemeKey);
            
            if (dekont == null)
            {
                return NotFound(new 
                { 
                    success = false, 
                    message = "Dekont bilgisi bulunamadý." 
                });
            }

            return Ok(dekont);
        }

        /// <summary>
        /// Ödeme durumunu günceller (3D Secure sonucu için).
        /// </summary>
        [HttpPut("guncelle")]
        public async Task<ActionResult<OdemeUpdateDto>> UpdateOdeme([FromBody] OdemeUpdateRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.OdemeId <= 0) missingFields.Add("OdemeId");
            if (string.IsNullOrWhiteSpace(request.OdemeKey)) missingFields.Add("OdemeKey");
            if (string.IsNullOrWhiteSpace(request.CMesaj)) missingFields.Add("CMesaj");
            if (string.IsNullOrWhiteSpace(request.BSonucDurum)) missingFields.Add("BSonucDurum");
            if (string.IsNullOrWhiteSpace(request.OSonuc)) missingFields.Add("OSonuc");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            var result = await _service.UpdateOdemeAsync(
                request.OdemeId,
                request.OdemeKey,
                request.CMesaj,
                request.BSonucDurum,
                request.OSonuc);

            if (result == null)
            {
                return NotFound(new 
                { 
                    success = false, 
                    message = "Ödeme bulunamadý veya daha önce iþlenmiþ." 
                });
            }

            return result.Sonuc switch
            {
                1 => Ok(new 
                { 
                    success = true, 
                    message = "Ödeme baþarýyla tamamlandý.",
                    data = result
                }),
                0 => Ok(new 
                { 
                    success = false, 
                    message = "Ödeme baþarýsýz.",
                    data = result
                }),
                _ => Ok(new 
                { 
                    success = false, 
                    message = "Ödeme durumu güncellenemedi.",
                    data = result
                })
            };
        }

        /// <summary>
        /// Param sanal pos log kaydý oluþturur.
        /// </summary>
        [HttpPost("param-log")]
        public async Task<ActionResult> InsertParamLog([FromBody] ParamLogRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.UyeId <= 0) missingFields.Add("UyeId");
            if (request.IslemId <= 0) missingFields.Add("IslemId");
            if (string.IsNullOrWhiteSpace(request.Sonuc)) missingFields.Add("Sonuc");
            if (string.IsNullOrWhiteSpace(request.SonucStr)) missingFields.Add("SonucStr");
            if (string.IsNullOrWhiteSpace(request.UcdUrl)) missingFields.Add("UcdUrl");
            if (string.IsNullOrWhiteSpace(request.SiparisId)) missingFields.Add("SiparisId");
            if (string.IsNullOrWhiteSpace(request.HostIpAdres)) missingFields.Add("HostIpAdres");
            if (string.IsNullOrWhiteSpace(request.ServerIPAdres)) missingFields.Add("ServerIPAdres");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            await _service.InsertParamLogAsync(
                request.UyeId,
                request.IslemId,
                request.Sonuc,
                request.SonucStr,
                request.BankaSonucKod,
                request.UcdUrl,
                request.SiparisId,
                request.HostIpAdres,
                request.ServerIPAdres);

            return Ok(new 
            { 
                success = true, 
                message = "Param log kaydý baþarýyla oluþturuldu." 
            });
        }

        /// <summary>
        /// Ozan ödeme durumunu günceller.
        /// </summary>
        [HttpPut("guncelle-ozan")]
        public async Task<ActionResult<OdemeUpdateDto>> UpdateOzanOdeme([FromBody] OzanOdemeUpdateRequest request)
        {
            var missingFields = new List<string>();
            
            if (request.OdemeId <= 0) missingFields.Add("OdemeId");
            if (string.IsNullOrWhiteSpace(request.OzanStatus)) missingFields.Add("OzanStatus");
            if (string.IsNullOrWhiteSpace(request.OzanMessage)) missingFields.Add("OzanMessage");
            if (string.IsNullOrWhiteSpace(request.OzanTransactionId)) missingFields.Add("OzanTransactionId");
            if (string.IsNullOrWhiteSpace(request.OzanCheckSum)) missingFields.Add("OzanCheckSum");
            if (string.IsNullOrWhiteSpace(request.OdemeKey)) missingFields.Add("OdemeKey");

            if (missingFields.Count > 0)
            {
                return BadRequest(new { success = false, message = "Zorunlu alanlar eksik.", missingFields });
            }

            var result = await _service.UpdateOzanOdemeAsync(
                request.OdemeId, request.OzanStatus, request.OzanMessage, request.OzanTransactionId,
                request.OzanOdemeTutari, request.OzanCheckSum, request.OzanCustomData,
                request.OdemeBasariliMi, request.OdemeKey);

            if (result == null)
            {
                return NotFound(new { success = false, message = "Ödeme bulunamadý veya daha önce iþlenmiþ." });
            }

            return result.Sonuc == 1
                ? Ok(new { success = true, message = "Ödeme baþarýyla tamamlandý.", data = result })
                : Ok(new { success = false, message = "Ödeme baþarýsýz.", data = result });
        }

        /// <summary>
        /// Param ödeme durumunu günceller.
        /// </summary>
        [HttpPut("guncelle-param")]
        public async Task<ActionResult<OdemeUpdateDto>> UpdateParamOdeme([FromBody] ParamOdemeUpdateRequest request)
        {
            var missingFields = new List<string>();
            
            if (request.OdemeId <= 0) missingFields.Add("OdemeId");
            if (string.IsNullOrWhiteSpace(request.ParamSonuc)) missingFields.Add("ParamSonuc");
            if (string.IsNullOrWhiteSpace(request.ParamDekontId)) missingFields.Add("ParamDekontId");
            if (string.IsNullOrWhiteSpace(request.ParamSiparisId)) missingFields.Add("ParamSiparisId");
            if (string.IsNullOrWhiteSpace(request.OdemeKey)) missingFields.Add("OdemeKey");

            if (missingFields.Count > 0)
            {
                return BadRequest(new { success = false, message = "Zorunlu alanlar eksik.", missingFields });
            }

            var result = await _service.UpdateParamOdemeAsync(
                request.OdemeId, request.ParamSonuc, request.ParamDekontId, request.ParamTahsilatTutari,
                request.ParamOdemeTutari, request.ParamSiparisId, request.ParamExtData,
                request.OdemeBasariliMi, request.OdemeKey);

            if (result == null)
            {
                return NotFound(new { success = false, message = "Ödeme bulunamadý veya daha önce iþlenmiþ." });
            }

            return result.Sonuc == 1
                ? Ok(new { success = true, message = "Ödeme baþarýyla tamamlandý.", data = result })
                : Ok(new { success = false, message = "Ödeme baþarýsýz.", data = result });
        }

        /// <summary>
        /// PayTr ödeme durumunu günceller.
        /// </summary>
        [HttpPut("guncelle-paytr")]
        public async Task<ActionResult<OdemeUpdateDto>> UpdatePayTrOdeme([FromBody] PayTrOdemeUpdateRequest request)
        {
            var missingFields = new List<string>();
            
            if (request.OdemeId <= 0) missingFields.Add("OdemeId");
            if (string.IsNullOrWhiteSpace(request.PayTrStatus)) missingFields.Add("PayTrStatus");
            if (string.IsNullOrWhiteSpace(request.PayTrHash)) missingFields.Add("PayTrHash");

            if (missingFields.Count > 0)
            {
                return BadRequest(new { success = false, message = "Zorunlu alanlar eksik.", missingFields });
            }

            var result = await _service.UpdatePayTrOdemeAsync(
                request.OdemeId, request.PayTrStatus, request.PayTrTotalAmount, request.PayTrHash,
                request.PayTrTestMode, request.PayTrPaymentType, request.PayTrCurrency,
                request.PayTrPaymentAmount, request.PayTrFailedCode, request.PayTrFailedMsg,
                request.OdemeBasariliMi);

            if (result == null)
            {
                return NotFound(new { success = false, message = "Ödeme bulunamadý veya daha önce iþlenmiþ." });
            }

            return result.Sonuc == 1
                ? Ok(new { success = true, message = "Ödeme baþarýyla tamamlandý.", data = result })
                : Ok(new { success = false, message = "Ödeme baþarýsýz.", data = result });
        }
    }

    public class OdemeCreateRequest
    {
        public int UyeId { get; set; }
        public decimal OdenecekTutar { get; set; }
        public int KrediKarti { get; set; }
        public int Taksit { get; set; }
        public string KartSahibi { get; set; } = string.Empty;
        public string KartNo { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
    }

    public class OdemeUpdateRequest
    {
        public int OdemeId { get; set; }
        public string OdemeKey { get; set; } = string.Empty;
        public string CMesaj { get; set; } = string.Empty;
        public string BSonucDurum { get; set; } = string.Empty;
        public string OSonuc { get; set; } = string.Empty;
    }

    public class ParamLogRequest
    {
        public int UyeId { get; set; }
        public long IslemId { get; set; }
        public string Sonuc { get; set; } = string.Empty;
        public string SonucStr { get; set; } = string.Empty;
        public int BankaSonucKod { get; set; }
        public string UcdUrl { get; set; } = string.Empty;
        public string SiparisId { get; set; } = string.Empty;
        public string HostIpAdres { get; set; } = string.Empty;
        public string ServerIPAdres { get; set; } = string.Empty;
    }

    public class OzanOdemeUpdateRequest
    {
        public int OdemeId { get; set; }
        public string OzanStatus { get; set; } = string.Empty;
        public string OzanMessage { get; set; } = string.Empty;
        public string OzanTransactionId { get; set; } = string.Empty;
        public decimal OzanOdemeTutari { get; set; }
        public string OzanCheckSum { get; set; } = string.Empty;
        public string OzanCustomData { get; set; } = string.Empty;
        public bool OdemeBasariliMi { get; set; }
        public string OdemeKey { get; set; } = string.Empty;
    }

    public class ParamOdemeUpdateRequest
    {
        public int OdemeId { get; set; }
        public string ParamSonuc { get; set; } = string.Empty;
        public string ParamDekontId { get; set; } = string.Empty;
        public decimal ParamTahsilatTutari { get; set; }
        public decimal ParamOdemeTutari { get; set; }
        public string ParamSiparisId { get; set; } = string.Empty;
        public string ParamExtData { get; set; } = string.Empty;
        public bool OdemeBasariliMi { get; set; }
        public string OdemeKey { get; set; } = string.Empty;
    }

    public class PayTrOdemeUpdateRequest
    {
        public int OdemeId { get; set; }
        public string PayTrStatus { get; set; } = string.Empty;
        public decimal PayTrTotalAmount { get; set; }
        public string PayTrHash { get; set; } = string.Empty;
        public bool PayTrTestMode { get; set; }
        public string PayTrPaymentType { get; set; } = string.Empty;
        public string PayTrCurrency { get; set; } = string.Empty;
        public int PayTrPaymentAmount { get; set; }
        public string PayTrFailedCode { get; set; } = string.Empty;
        public string PayTrFailedMsg { get; set; } = string.Empty;
        public bool OdemeBasariliMi { get; set; }
    }
}
