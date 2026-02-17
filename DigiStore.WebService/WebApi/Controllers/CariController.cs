using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;

namespace DigiStore.WebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CariController : ControllerBase
    {
        private readonly ICariService _service;
        
        public CariController(ICariService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cari bilgilerini getirir.
        /// </summary>
        [HttpGet("bilgilerim/{uyeId}")]
        public async Task<ActionResult<CariBilgilerDto?>> GetCariBilgilerim(int uyeId)
        {
            var dto = await _service.GetCariBilgilerimAsync(uyeId);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Cari bilgileri bulunamadý." });
            return Ok(dto);
        }

        /// <summary>
        /// Cari genel durum (aylýk özet) bilgilerini getirir.
        /// </summary>
        [HttpGet("genel-durum/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariGenelDurumDto>>> GetCariGenelDurum(int uyeId, [FromQuery] int yil, [FromQuery] int currencyId)
        {
            var list = await _service.GetCariGenelDurumAsync(uyeId, yil, currencyId);
            return Ok(list);
        }

        /// <summary>
        /// Belirli tarih aralýðý için cari genel durum detayýný getirir. (SeriNo ilk hanesi hareket tipini, kalaný hareket ID belirtir. Ýlk hane 1=Ödeme 2=Tahsilat 3=Sipariþ 4=Alýþ)
        /// </summary>
        [HttpGet("genel-durum-detay/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariGenelDurumDetayDto>>> GetCariGenelDurumDetay(int uyeId, [FromQuery] DateTime dateBas, [FromQuery] DateTime dateSon, [FromQuery] int currencyId)
        {
            var list = await _service.GetCariGenelDurumDetayAsync(uyeId, dateBas, dateSon, currencyId);
            return Ok(list);
        }

        /// <summary>
        /// /// Tüm üyeler için belirli tarih aralýðý ve para birimi bazýnda cari genel durum detayýný getirir. Varsayýlan: dateBas=1900-01-01, dateSon=bugün, currencyId=null (tümü TL'ye çevrilir)
        /// </summary>
        [HttpGet("genel-durum-detay-all")]
        public async Task<ActionResult<IEnumerable<CariGenelDurumDetayDto>>> GetCariGenelDurumDetayAll([FromQuery] DateTime? dateBas = null, [FromQuery] DateTime? dateSon = null, [FromQuery] int? currencyId = null)
        {
            var list = await _service.GetCariGenelDurumDetayAllAsync(dateBas, dateSon, currencyId);
            return Ok(list);
        }

        /// <summary>
        /// Cari hareket satýr detaylarýný getirir.
        /// </summary>
        [HttpGet("hareket/{hareketId}/user/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariHareketDetayDto>>> GetCariHareketById(int hareketId, int uyeId)
        {
            var details = await _service.GetCariHareketByIdAsync(hareketId, uyeId);
            if (details == null || details.Count == 0) return NotFound(new { error = "Bulunamadý", message = "Cari hareket detaylarý bulunamadý." });
            return Ok(details);
        }

        /// <summary>
        /// Cari hareket listesini getirir. (Tarih periyodu: 0=Bugün 1=Ýki gün 2=Üç gün 3=Bir hafta 4=Ýki hafta 5=Bir ay 6=Üç ay 7=Bir yýl)
        /// </summary>
        [HttpGet("hareketler/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariHareketDto>>> GetCariHareketler(int uyeId, [FromQuery] int tarih)
        {
            var list = await _service.GetCariHareketlerAsync(uyeId, tarih);
            return Ok(list);
        }

        /// <summary>
        /// Cari para birimi seçeneklerini getirir.
        /// </summary>
        [HttpGet("para-birimleri/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariParaBirimiDto>>> GetCariParaBirimleri(int uyeId)
        {
            var list = await _service.GetCariParaBirimleriAsync(uyeId);
            return Ok(list);
        }

        /// <summary>
        /// Bayi doðrulama bilgilerini getirir.
        /// </summary>
        [HttpPost("dogrulama")]
        public async Task<ActionResult<BayiBilgileriDto?>> Dogrulama([FromBody] BayiDogrulamaRequest request)
        {
            var dto = await _service.GetBayiDogrulamaAsync(request.BayiKodu, request.Eposta, request.Parola);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Bayi doðrulama baþarýsýz." });
            return Ok(dto);
        }

        /// <summary>
        /// ReCaptcha skorunu kaydeder (login sýrasýnda).
        /// </summary>
        [HttpPost("recaptcha-skor")]
        public async Task<ActionResult> InsertReCaptchaSkor([FromBody] ReCaptchaSkorRequest request)
        {
            await _service.InsertReCaptchaSkorAsync(request.RcSkor, request.BayiKodu, request.EPosta, request.Sifre);
            return Ok(new { success = true, message = "ReCaptcha skoru kaydedildi." });
        }

        /// <summary>
        /// Günü gelen ödemeler: ilk sonuç detay, ikinci sonuç özet.
        /// </summary>
        [HttpGet("gunu-gelen-odemeler/{uyeId}")]
        public async Task<ActionResult<object>> GetGunuGelenOdemeler(int uyeId)
        {
            var (detaylar, ozet) = await _service.GetGunuGelenOdemelerAsync(uyeId);
            return Ok(new { detaylar, ozet });
        }

        /// <summary>
        /// Sipariþ listeleme.
        /// </summary>
        [HttpGet("siparisler/{uyeId}")]
        public async Task<ActionResult<IEnumerable<SiparisDto>>> GetSiparislerim(int uyeId, [FromQuery] DateTime dateBas, [FromQuery] DateTime dateSon)
        {
            var list = await _service.GetSiparislerimAsync(uyeId, dateBas, dateSon);
            return Ok(list);
        }

        /// <summary>
        /// Sipariþ detay: 1) baþlýk, 2) ürünler, 3) tutarlar.
        /// </summary>
        [HttpGet("siparis/{satisId}/user/{uyeId}")]
        public async Task<ActionResult<object>> GetSiparisDetay(int satisId, int uyeId)
        {
            var (siparis, urunler, tutarlar) = await _service.GetSiparisDetayAsync(satisId, uyeId);
            if (siparis == null) return NotFound(new { error = "Bulunamadý", message = "Sipariþ bulunamadý." });
            return Ok(new { siparis, urunler, tutarlar });
        }

        /// <summary>
        /// Üye bilgileri ve tutar bilgilerini getirir.
        /// </summary>
        [HttpGet("uye-bilgileri/{uyeId}")]
        public async Task<ActionResult<CariBilgilerTutarDto?>> GetUyeBilgileri(int uyeId)
        {
            var dto = await _service.GetUyeBilgileriAsync(uyeId);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Üye bilgileri bulunamadý." });
            return Ok(dto);
        }

        /// <summary>
        /// Üye baþvurusu oluþturur (Vergi Levhasý dosya yükleme ile). Kabul edilen formatlar: PDF, JPG, JPEG, PNG, GIF, BMP, WEBP
        /// </summary>
        [HttpPost("uye-basvuru")]
        public async Task<ActionResult> UyeBasvuru([FromForm] UyeBasvuruRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.Dosya == null || request.Dosya.Length == 0) missingFields.Add("Dosya");
            if (string.IsNullOrWhiteSpace(request.FirmaAdi)) missingFields.Add("FirmaAdi");
            if (string.IsNullOrWhiteSpace(request.FirmaYetkili)) missingFields.Add("FirmaYetkili");
            if (string.IsNullOrWhiteSpace(request.Email)) missingFields.Add("Email");
            if (string.IsNullOrWhiteSpace(request.Sifre)) missingFields.Add("Sifre");
            if (string.IsNullOrWhiteSpace(request.Telefon)) missingFields.Add("Telefon");
            if (string.IsNullOrWhiteSpace(request.Adres)) missingFields.Add("Adres");
            if (string.IsNullOrWhiteSpace(request.VergiDairesi)) missingFields.Add("VergiDairesi");
            if (string.IsNullOrWhiteSpace(request.VergiNo)) missingFields.Add("VergiNo");
            if (request.SehirId <= 0) missingFields.Add("SehirId");
            if (request.IlceId <= 0) missingFields.Add("IlceId");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            // File extension validation
            if (request.Dosya != null)
            {
                var fileExtension = Path.GetExtension(request.Dosya.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = "Geçersiz dosya formatý. Ýzin verilen formatlar: PDF, JPG, JPEG, PNG, GIF, BMP, WEBP",
                        field = "Dosya",
                        receivedExtension = fileExtension,
                        allowedExtensions = allowedExtensions
                    });
                }

                // File size validation (max 10 MB)
                const long maxFileSize = 10 * 1024 * 1024; // 10 MB in bytes
                if (request.Dosya.Length > maxFileSize)
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = $"Dosya boyutu çok büyük. Maksimum izin verilen boyut: {maxFileSize / 1024 / 1024} MB",
                        field = "Dosya",
                        fileSize = request.Dosya.Length,
                        maxSize = maxFileSize
                    });
                }

                // Content type validation (additional security check)
                var allowedContentTypes = new[] 
                { 
                    "application/pdf",
                    "image/jpeg",
                    "image/jpg", 
                    "image/png", 
                    "image/gif", 
                    "image/bmp",
                    "image/webp"
                };
                
                if (!allowedContentTypes.Contains(request.Dosya.ContentType.ToLowerInvariant()))
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = "Geçersiz dosya tipi. Lütfen geçerli bir PDF veya resim dosyasý yükleyin.",
                        field = "Dosya",
                        receivedContentType = request.Dosya.ContentType
                    });
                }
            }

            // Email validation
            if (!IsValidEmail(request.Email))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Geçersiz email formatý. Örnek: ornek@host.com",
                    field = "Email"
                });
            }

            // Phone validation (Turkish format: +905XXXXXXXXX)
            if (!IsValidTurkishPhone(request.Telefon))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Geçersiz telefon formatý. Örnek: +905112223344 (baþýnda +90 olmalý ve 13 karakter olmalýdýr)",
                    field = "Telefon"
                });
            }

            byte[] dosyaBytes;
            using (var ms = new MemoryStream())
            {
                await request.Dosya!.CopyToAsync(ms);
                dosyaBytes = ms.ToArray();
            }

            var result = await _service.InsertUyeBasvuruAsync(
                dosyaBytes,
                request.Dosya.FileName,
                request.FirmaAdi,
                request.FirmaYetkili,
                request.Email,
                request.Sifre,
                request.Telefon,
                request.Adres,
                request.VergiDairesi,
                request.VergiNo,
                request.SehirId,
                request.IlceId);

            return result switch
            {
                1 => Ok(new { success = true, message = "Baþvurunuz alýnmýþtýr.", code = 1 }),
                3 => BadRequest(new { success = false, message = "Bu vergi numarasý ile kayýtlý üye bulunmaktadýr.", code = 3 }),
                _ => StatusCode(500, new { success = false, message = "Baþvuru iþlemi baþarýsýz.", code = 0 })
            };
        }

        /// <summary>
        /// Üye þifre güncelleme (doðrulama kodu ile).
        /// </summary>
        [HttpPost("uye-sifre-guncelle")]
        public async Task<ActionResult> UpdateUyeSifre([FromBody] UyeSifreGuncelleRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.UyeId <= 0) missingFields.Add("UyeId");
            if (string.IsNullOrWhiteSpace(request.VKod)) missingFields.Add("VKod");
            if (string.IsNullOrWhiteSpace(request.Sifre)) missingFields.Add("Sifre");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            var result = await _service.UpdateUyeSifreAsync(request.UyeId, request.VKod, request.Sifre);

            return result switch
            {
                1 => Ok(new { success = true, message = "Þifreniz baþarýyla güncellenmiþtir.", code = 1 }),
                -1 => BadRequest(new { success = false, message = "Geçersiz doðrulama kodu veya üye bulunamadý.", code = -1 }),
                _ => StatusCode(500, new { success = false, message = "Þifre güncelleme iþlemi baþarýsýz.", code = 0 })
            };
        }

        /// <summary>
        /// Þifre sýfýrlama kodu oluþturur ve döndürür.
        /// </summary>
        [HttpPost("parola-yenileme-kodu")]
        public async Task<ActionResult> GetParolaYenilemeKodu([FromBody] ParolaYenilemeKoduRequest request)
        {
            // Email validation
            if (string.IsNullOrWhiteSpace(request.EPosta))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "E-posta adresi gereklidir.",
                    field = "EPosta"
                });
            }

            if (!IsValidEmail(request.EPosta))
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Geçersiz email formatý. Örnek: ornek@host.com",
                    field = "EPosta"
                });
            }

            var result = await _service.GetParolaYenilemeKoduAsync(request.EPosta);
            
            if (result == null)
            {
                return NotFound(new 
                { 
                    success = false, 
                    message = "Bu e-posta adresi ile kayýtlý üye bulunamadý." 
                });
            }

            return Ok(new 
            { 
                success = true, 
                message = "Þifre yenileme kodu oluþturuldu.",
                data = result
            });
        }

        /// <summary>
        /// Þirket logo yolunu günceller.
        /// </summary>
        [HttpPut("sirket-logo")]
        public async Task<ActionResult> UpdateSirketLogo([FromBody] SirketLogoRequest request)
        {
            // Required field validation
            var missingFields = new List<string>();
            
            if (request.UyeId <= 0) missingFields.Add("UyeId");
            if (string.IsNullOrWhiteSpace(request.Path)) missingFields.Add("Path");

            if (missingFields.Count > 0)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "Zorunlu alanlar eksik.", 
                    missingFields = missingFields 
                });
            }

            await _service.UpdateSirketLogoAsync(request.UyeId, request.Path);

            return Ok(new 
            { 
                success = true, 
                message = "Þirket logosu baþarýyla güncellendi." 
            });
        }

        /// <summary>
        /// Üyenin ödemelerini tarih aralýðýna göre getirir.
        /// </summary>
        [HttpGet("odemelerim/{uyeId}")]
        public async Task<ActionResult<IEnumerable<CariOdemeDto>>> GetOdemelerim(
            int uyeId, 
            [FromQuery] DateTime dateBas, 
            [FromQuery] DateTime dateSon)
        {
            var list = await _service.GetOdemelerimAsync(uyeId, dateBas, dateSon);
            return Ok(list);
        }

        /// <summary>
        /// Ödeme detayýný getirir.
        /// </summary>
        [HttpGet("odeme/{paraHareketiId}/user/{uyeId}")]
        public async Task<ActionResult<CariOdemeDetayDto>> GetOdemeDetay(int paraHareketiId, int uyeId)
        {
            var dto = await _service.GetOdemeDetayAsync(paraHareketiId, uyeId);
            if (dto == null) return NotFound(new { error = "Bulunamadý", message = "Ödeme detayý bulunamadý." });
            return Ok(dto);
        }

        /// <summary>
        /// Tüm üyeleri getirir.
        /// </summary>
        [HttpGet("tum-uyeler")]
        public async Task<ActionResult<IEnumerable<TumUyelerDto>>> GetTumUyeler()
        {
            var list = await _service.GetTumUyelerAsync();
            return Ok(list);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains('@') && email.Contains('.');
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidTurkishPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Turkish format: +905XXXXXXXXX (13 characters total)
            // Should start with +90 and be followed by 10 digits
            if (!phone.StartsWith("+90"))
                return false;

            if (phone.Length != 13)
                return false;

            // Check if all characters after +90 are digits
            for (int i = 3; i < phone.Length; i++)
            {
                if (!char.IsDigit(phone[i]))
                    return false;
            }

            // Optionally: Check if mobile number starts with 5 (Turkish mobile numbers)
            // Uncomment below if you want to enforce mobile numbers only
            // if (phone.Length > 3 && phone[3] != '5')
            //     return false;

            return true;
        }
    }

    public class BayiDogrulamaRequest
    {
        public string BayiKodu { get; set; } = string.Empty;
        public string Eposta { get; set; } = string.Empty;
        public string Parola { get; set; } = string.Empty;
    }

    public class ReCaptchaSkorRequest
    {
        public decimal RcSkor { get; set; }
        public string BayiKodu { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }

    public class UyeBasvuruRequest
    {
        public IFormFile? Dosya { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public string FirmaYetkili { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string VergiDairesi { get; set; } = string.Empty;
        public string VergiNo { get; set; } = string.Empty;
        public int SehirId { get; set; }
        public int IlceId { get; set; }
    }

    public class UyeSifreGuncelleRequest
    {
        public int UyeId { get; set; }
        public string VKod { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }

    public class ParolaYenilemeKoduRequest
    {
        public string EPosta { get; set; } = string.Empty;
    }

    public class SirketLogoRequest
    {
        public int UyeId { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
