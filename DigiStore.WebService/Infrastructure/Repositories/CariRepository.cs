using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class CariRepository
    {
        private readonly ApplicationDbContext _db;
        public CariRepository(ApplicationDbContext db) { _db = db; }

        public async Task<CariBilgilerDto?> GetCariBilgilerimAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariBilgilerim";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CariBilgilerDto
                {
                    UyeId = uyeId,
                    Unvan = reader.IsDBNull(reader.GetOrdinal("Unvan")) ? null : reader.GetString(reader.GetOrdinal("Unvan")),
                    VergiDairesi = reader.IsDBNull(reader.GetOrdinal("VergiDairesi")) ? null : reader.GetString(reader.GetOrdinal("VergiDairesi")),
                    VergiNo = reader.IsDBNull(reader.GetOrdinal("VergiNo")) ? null : reader.GetString(reader.GetOrdinal("VergiNo")),
                    Adres = reader.IsDBNull(reader.GetOrdinal("Adres")) ? null : reader.GetString(reader.GetOrdinal("Adres")),
                    Telefon = reader.IsDBNull(reader.GetOrdinal("Telefon")) ? null : reader.GetString(reader.GetOrdinal("Telefon")),
                    EPosta = reader.IsDBNull(reader.GetOrdinal("EPosta")) ? null : reader.GetString(reader.GetOrdinal("EPosta"))
                };
            }
            return null;
        }

        public async Task<List<CariGenelDurumDto>> GetCariGenelDurumAsync(int uyeId, int yil, int currencyId)
        {
            var list = new List<CariGenelDurumDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariGenelDurum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@Yil", SqlDbType.Int) { Value = yil });
            cmd.Parameters.Add(new SqlParameter("@CurrencyId", SqlDbType.Int) { Value = currencyId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CariGenelDurumDto
                {
                    Ay = reader.IsDBNull(reader.GetOrdinal("Ay")) ? null : reader.GetString(reader.GetOrdinal("Ay")),
                    Ayno = reader.IsDBNull(reader.GetOrdinal("Ayno")) ? null : reader.GetInt32(reader.GetOrdinal("Ayno")),
                    Borc = reader.IsDBNull(reader.GetOrdinal("Borc")) ? null : reader.GetDecimal(reader.GetOrdinal("Borc")),
                    Alacak = reader.IsDBNull(reader.GetOrdinal("Alacak")) ? null : reader.GetDecimal(reader.GetOrdinal("Alacak")),
                    Bakiye = reader.IsDBNull(reader.GetOrdinal("Bakiye")) ? null : reader.GetDecimal(reader.GetOrdinal("Bakiye"))
                });
            }
            return list;
        }

        public async Task<List<CariGenelDurumDetayDto>> GetCariGenelDurumDetayAsync(int uyeId, DateTime dateBas, DateTime dateSon, int currencyId)
        {
            var list = new List<CariGenelDurumDetayDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariGenelDurumDetay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@DateBas", SqlDbType.Date) { Value = dateBas });
            cmd.Parameters.Add(new SqlParameter("@DateSon", SqlDbType.Date) { Value = dateSon });
            cmd.Parameters.Add(new SqlParameter("@CurrencyId", SqlDbType.Int) { Value = currencyId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CariGenelDurumDetayDto
                {
                    Tarih = reader.IsDBNull(reader.GetOrdinal("Tarih")) ? null : reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    SeriNo = reader.IsDBNull(reader.GetOrdinal("SeriNo")) ? null : reader.GetInt32(reader.GetOrdinal("SeriNo")),
                    HareketTipi = reader.IsDBNull(reader.GetOrdinal("HareketTipi")) ? null : reader.GetString(reader.GetOrdinal("HareketTipi")),
                    Borc = reader.IsDBNull(reader.GetOrdinal("Borc")) ? null : reader.GetDecimal(reader.GetOrdinal("Borc")),
                    Alacak = reader.IsDBNull(reader.GetOrdinal("Alacak")) ? null : reader.GetDecimal(reader.GetOrdinal("Alacak")),
                    Bakiye = reader.IsDBNull(reader.GetOrdinal("Bakiye")) ? null : reader.GetDecimal(reader.GetOrdinal("Bakiye"))
                });
            }
            return list;
        }

        public async Task<List<CariHareketDetayDto>> GetCariHareketByIdAsync(int hareketId, int uyeId)
        {
            var details = new List<CariHareketDetayDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariHareketById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@HareketId", SqlDbType.Int) { Value = hareketId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                details.Add(new CariHareketDetayDto
                {
                    SatisId = reader.GetInt32(reader.GetOrdinal("SatisId")),
                    SatisLinesId = reader.GetInt32(reader.GetOrdinal("SatisLinesId")),
                    SiparisTarihi = reader.IsDBNull(reader.GetOrdinal("SiparisTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("SiparisTarihi")),
                    UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                    UrunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? null : reader.GetString(reader.GetOrdinal("UrunAdi")),
                    Miktar = reader.GetDecimal(reader.GetOrdinal("Miktar")),
                    BirimFiyat = reader.IsDBNull(reader.GetOrdinal("BirimFiyat")) ? null : reader.GetDecimal(reader.GetOrdinal("BirimFiyat")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    SatirTutar = reader.IsDBNull(reader.GetOrdinal("SatirTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("SatirTutar")),
                    Barkodlar = reader.IsDBNull(reader.GetOrdinal("Barkodlar")) ? null : reader.GetString(reader.GetOrdinal("Barkodlar")),
                    Iskonto = reader.IsDBNull(reader.GetOrdinal("Iskonto")) ? null : reader.GetDecimal(reader.GetOrdinal("Iskonto")),
                    DovizKuru = reader.IsDBNull(reader.GetOrdinal("DovizKuru")) ? null : reader.GetDecimal(reader.GetOrdinal("DovizKuru")),
                    ToplamTutar = reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamTutar")),
                    ToplamTutarTL = reader.IsDBNull(reader.GetOrdinal("ToplamTutarTL")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamTutarTL")),
                    ClassIsTL = reader.IsDBNull(reader.GetOrdinal("ClassIsTL")) ? null : reader.GetString(reader.GetOrdinal("ClassIsTL"))
                });
            }

            return details;
        }

        public async Task<List<CariHareketDto>> GetCariHareketlerAsync(int uyeId, int tarih)
        {
            var list = new List<CariHareketDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariHareketler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@tarih", SqlDbType.Int) { Value = tarih });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CariHareketDto
                {
                    Tarih = reader.IsDBNull(reader.GetOrdinal("Tarih")) ? null : reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Tip = reader.IsDBNull(reader.GetOrdinal("Tip")) ? null : reader.GetInt32(reader.GetOrdinal("Tip")),
                    FaturaNo = reader.IsDBNull(reader.GetOrdinal("FaturaNo")) ? null : reader.GetString(reader.GetOrdinal("FaturaNo")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    URL = reader.IsDBNull(reader.GetOrdinal("URL")) ? null : reader.GetString(reader.GetOrdinal("URL")),
                    SeriNo = reader.IsDBNull(reader.GetOrdinal("SeriNo")) ? null : reader.GetString(reader.GetOrdinal("SeriNo")),
                    HareketTipi = reader.IsDBNull(reader.GetOrdinal("HareketTipi")) ? null : reader.GetString(reader.GetOrdinal("HareketTipi")),
                    ClassPdfIcon = reader.IsDBNull(reader.GetOrdinal("ClassPdfIcon")) ? null : reader.GetString(reader.GetOrdinal("ClassPdfIcon")),
                    ClassModal = reader.IsDBNull(reader.GetOrdinal("ClassModal")) ? null : reader.GetString(reader.GetOrdinal("ClassModal")),
                    Tutar = reader.IsDBNull(reader.GetOrdinal("Tutar")) ? null : reader.GetDecimal(reader.GetOrdinal("Tutar"))
                });
            }
            return list;
        }

        public async Task<List<CariParaBirimiDto>> GetCariParaBirimleriAsync(int uyeId)
        {
            var list = new List<CariParaBirimiDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCariParaBirimleri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CariParaBirimiDto
                {
                    CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                    Abbr = reader.IsDBNull(reader.GetOrdinal("Abbr")) ? null : reader.GetString(reader.GetOrdinal("Abbr"))
                });
            }
            return list;
        }

        public async Task<BayiBilgileriDto?> GetBayiDogrulamaAsync(string bayiKodu, string eposta, string parola)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetBayiDogrulama";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@BayiKodu", SqlDbType.VarChar, 255) { Value = bayiKodu });
            cmd.Parameters.Add(new SqlParameter("@Eposta", SqlDbType.VarChar, 255) { Value = eposta });
            cmd.Parameters.Add(new SqlParameter("@Parola", SqlDbType.VarChar, 255) { Value = parola });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var dto = new BayiBilgileriDto
                {
                    UyeId = reader.GetOrdinal("UyeId") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UyeId")) ? reader.GetInt32(reader.GetOrdinal("UyeId")) : 0,
                    Ad = reader.GetOrdinal("Ad") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Ad")) ? reader.GetString(reader.GetOrdinal("Ad")) : null,
                    Soyad = reader.GetOrdinal("Soyad") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Soyad")) ? reader.GetString(reader.GetOrdinal("Soyad")) : null,
                    Name = reader.GetOrdinal("Name") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Name")) ? reader.GetString(reader.GetOrdinal("Name")) : null,
                    Eposta = reader.GetOrdinal("Eposta") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Eposta")) ? reader.GetString(reader.GetOrdinal("Eposta")) : null,
                    Parola = reader.GetOrdinal("Parola") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Parola")) ? reader.GetString(reader.GetOrdinal("Parola")) : null,
                    DogrulandiMi = reader.GetOrdinal("DogrulandiMi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("DogrulandiMi")) && reader.GetBoolean(reader.GetOrdinal("DogrulandiMi")),
                    KullaniciId = reader.GetOrdinal("KullaniciId") >= 0 && !reader.IsDBNull(reader.GetOrdinal("KullaniciId")) ? reader.GetInt32(reader.GetOrdinal("KullaniciId")) : 0,
                    AdSoyad = reader.GetOrdinal("AdSoyad") >= 0 && !reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? reader.GetString(reader.GetOrdinal("AdSoyad")) : null,
                    Telefon = reader.GetOrdinal("Telefon") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Telefon")) ? reader.GetString(reader.GetOrdinal("Telefon")) : null,
                    ResimYolu = reader.GetOrdinal("ResimYolu") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ResimYolu")) ? reader.GetString(reader.GetOrdinal("ResimYolu")) : null,
                    BegeniAdet = reader.GetOrdinal("BegeniAdet") >= 0 && !reader.IsDBNull(reader.GetOrdinal("BegeniAdet")) ? reader.GetInt32(reader.GetOrdinal("BegeniAdet")) : (int?)null,
                    SepetAdet = reader.GetOrdinal("SepetAdet") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SepetAdet")) ? reader.GetInt32(reader.GetOrdinal("SepetAdet")) : (int?)null,
                    ToplamTutar = reader.GetOrdinal("ToplamTutar") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? reader.GetDecimal(reader.GetOrdinal("ToplamTutar")) : (decimal?)null,
                    ParaBirimi = reader.GetOrdinal("ParaBirimi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    SirketLogoPath = reader.GetOrdinal("SirketLogoPath") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SirketLogoPath")) ? reader.GetString(reader.GetOrdinal("SirketLogoPath")) : null
                };
                return dto;
            }

            return null;
        }

        public async Task InsertReCaptchaSkorAsync(decimal rcSkor, string bayiKodu, string eposta, string sifre)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsRcSkor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RcSkor", SqlDbType.Decimal) { Value = rcSkor });
            cmd.Parameters.Add(new SqlParameter("@BayiKodu", SqlDbType.VarChar, 50) { Value = bayiKodu });
            cmd.Parameters.Add(new SqlParameter("@EPosta", SqlDbType.VarChar, 50) { Value = eposta });
            cmd.Parameters.Add(new SqlParameter("@Sifre", SqlDbType.VarChar, 50) { Value = sifre });
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<(List<GunuGelenOdemeDetayDto> detaylar, List<GunuGelenOdemeDto> ozet)> GetGunuGelenOdemelerAsync(int uyeId)
        {
            var detaylar = new List<GunuGelenOdemeDetayDto>();
            var ozet = new List<GunuGelenOdemeDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetGunuGelenOdemeler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                detaylar.Add(new GunuGelenOdemeDetayDto
                {
                    Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                    Tarih = reader.IsDBNull(reader.GetOrdinal("Tarih")) ? null : reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    Tutar = reader.IsDBNull(reader.GetOrdinal("Tutar")) ? null : reader.GetDecimal(reader.GetOrdinal("Tutar")),
                    CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? null : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                    HareketTipi = reader.IsDBNull(reader.GetOrdinal("HareketTipi")) ? null : reader.GetString(reader.GetOrdinal("HareketTipi")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    PbSembol = reader.IsDBNull(reader.GetOrdinal("PbSembol")) ? null : reader.GetString(reader.GetOrdinal("PbSembol")),
                    UyeCurr = reader.IsDBNull(reader.GetOrdinal("UyeCurr")) ? null : reader.GetInt32(reader.GetOrdinal("UyeCurr")),
                    UyeParaBirimi = reader.IsDBNull(reader.GetOrdinal("UyeParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("UyeParaBirimi")),
                    KalanTutar = reader.IsDBNull(reader.GetOrdinal("KalanTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("KalanTutar")),
                    KalanTutarTL = reader.IsDBNull(reader.GetOrdinal("KalanTutarTL")) ? null : reader.GetDecimal(reader.GetOrdinal("KalanTutarTL")),
                    VadeTarihi = reader.IsDBNull(reader.GetOrdinal("VadeTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("VadeTarihi")),
                    VadeGecenGun = reader.IsDBNull(reader.GetOrdinal("VadeGecenGun")) ? null : reader.GetInt32(reader.GetOrdinal("VadeGecenGun")),
                    VadeGecmisMi = reader.IsDBNull(reader.GetOrdinal("VadeGecmisMi")) ? null : reader.GetString(reader.GetOrdinal("VadeGecmisMi")),
                    ToplamTutar = reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamTutar"))
                });
            }

            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    ozet.Add(new GunuGelenOdemeDto
                    {
                        CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? null : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                        GunuGelenTutar = reader.IsDBNull(reader.GetOrdinal("GunuGelenTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("GunuGelenTutar")),
                        PbSembol = reader.IsDBNull(reader.GetOrdinal("PbSembol")) ? null : reader.GetString(reader.GetOrdinal("PbSembol")),
                        Abbr = reader.IsDBNull(reader.GetOrdinal("Abbr")) ? null : reader.GetString(reader.GetOrdinal("Abbr"))
                    });
                }
            }

            return (detaylar, ozet);
        }

        public async Task<List<SiparisDto>> GetSiparislerimAsync(int uyeId, DateTime dateBas, DateTime dateSon)
        {
            var list = new List<SiparisDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSiparislerim";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@DateBas", SqlDbType.Date) { Value = dateBas });
            cmd.Parameters.Add(new SqlParameter("@DateSon", SqlDbType.Date) { Value = dateSon });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new SiparisDto
                {
                    SatisId = reader.GetInt32(reader.GetOrdinal("SatisId")),
                    SiparisTarihi = reader.IsDBNull(reader.GetOrdinal("SiparisTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("SiparisTarihi")),
                    OdemeTipi = reader.IsDBNull(reader.GetOrdinal("OdemeTipi")) ? null : reader.GetString(reader.GetOrdinal("OdemeTipi")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    Tutar = reader.IsDBNull(reader.GetOrdinal("Tutar")) ? null : reader.GetDecimal(reader.GetOrdinal("Tutar")),
                    ToplamTutar = reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? null : reader.GetString(reader.GetOrdinal("ToplamTutar"))
                });
            }
            return list;
        }

        public async Task<(SiparisDetayDto? siparis, List<SiparisDetayUrunDto> urunler, List<SiparisDetayTutarDto> tutarlar)> GetSiparisDetayAsync(int satisId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSiparisDetay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SatisId", SqlDbType.Int) { Value = satisId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            SiparisDetayDto? siparis = null;
            if (await reader.ReadAsync())
            {
                siparis = new SiparisDetayDto
                {
                    SatisId = reader.GetInt32(reader.GetOrdinal("SatisId")),
                    SiparisTarihi = reader.IsDBNull(reader.GetOrdinal("SiparisTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("SiparisTarihi")),
                    SiparisDurumId = reader.IsDBNull(reader.GetOrdinal("SiparisDurumId")) ? null : reader.GetInt32(reader.GetOrdinal("SiparisDurumId")),
                    CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? null : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                    DovizKuru = reader.IsDBNull(reader.GetOrdinal("DovizKuru")) ? null : reader.GetDecimal(reader.GetOrdinal("DovizKuru")),
                    SiparisDurum = reader.IsDBNull(reader.GetOrdinal("SiparisDurum")) ? null : reader.GetString(reader.GetOrdinal("SiparisDurum")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    TeslimatAdresId = reader.IsDBNull(reader.GetOrdinal("TeslimatAdresId")) ? null : reader.GetInt32(reader.GetOrdinal("TeslimatAdresId")),
                    UyeId = reader.IsDBNull(reader.GetOrdinal("UyeId")) ? null : reader.GetInt32(reader.GetOrdinal("UyeId")),
                    Kargo = reader.IsDBNull(reader.GetOrdinal("Kargo")) ? null : reader.GetString(reader.GetOrdinal("Kargo")),
                    OdemeTipi = reader.IsDBNull(reader.GetOrdinal("OdemeTipi")) ? null : reader.GetString(reader.GetOrdinal("OdemeTipi")),
                    KargoTipi = reader.IsDBNull(reader.GetOrdinal("KargoTipi")) ? null : reader.GetString(reader.GetOrdinal("KargoTipi")),
                    KargoTakipNo = reader.IsDBNull(reader.GetOrdinal("KargoTakipNo")) ? null : reader.GetString(reader.GetOrdinal("KargoTakipNo")),
                    KargoUrl = reader.IsDBNull(reader.GetOrdinal("KargoUrl")) ? null : reader.GetString(reader.GetOrdinal("KargoUrl")),
                    Tutar = reader.IsDBNull(reader.GetOrdinal("Tutar")) ? null : reader.GetDecimal(reader.GetOrdinal("Tutar")),
                    GonderildiMi = reader.IsDBNull(reader.GetOrdinal("GonderildiMi")) ? null : reader.GetString(reader.GetOrdinal("GonderildiMi")),
                    TAdSoyad = reader.IsDBNull(reader.GetOrdinal("TAdSoyad")) ? null : reader.GetString(reader.GetOrdinal("TAdSoyad")),
                    TUnvan = reader.IsDBNull(reader.GetOrdinal("TUnvan")) ? null : reader.GetString(reader.GetOrdinal("TUnvan")),
                    TVergiDairesi = reader.IsDBNull(reader.GetOrdinal("TVergiDairesi")) ? null : reader.GetString(reader.GetOrdinal("TVergiDairesi")),
                    TVergiNo = reader.IsDBNull(reader.GetOrdinal("TVergiNo")) ? null : reader.GetString(reader.GetOrdinal("TVergiNo")),
                    TSehirAdi = reader.IsDBNull(reader.GetOrdinal("TSehirAdi")) ? null : reader.GetString(reader.GetOrdinal("TSehirAdi")),
                    TIlceAdi = reader.IsDBNull(reader.GetOrdinal("TIlceAdi")) ? null : reader.GetString(reader.GetOrdinal("TIlceAdi")),
                    TSahisAdres = reader.IsDBNull(reader.GetOrdinal("TSahisAdres")) ? null : reader.GetString(reader.GetOrdinal("TSahisAdres")),
                    TTelefon = reader.IsDBNull(reader.GetOrdinal("TTelefon")) ? null : reader.GetString(reader.GetOrdinal("TTelefon")),
                    AdSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? null : reader.GetString(reader.GetOrdinal("AdSoyad")),
                    Unvan = reader.IsDBNull(reader.GetOrdinal("Unvan")) ? null : reader.GetString(reader.GetOrdinal("Unvan")),
                    VergiDairesi = reader.IsDBNull(reader.GetOrdinal("VergiDairesi")) ? null : reader.GetString(reader.GetOrdinal("VergiDairesi")),
                    VergiNo = reader.IsDBNull(reader.GetOrdinal("VergiNo")) ? null : reader.GetString(reader.GetOrdinal("VergiNo")),
                    SehirAdi = reader.IsDBNull(reader.GetOrdinal("SehirAdi")) ? null : reader.GetString(reader.GetOrdinal("SehirAdi")),
                    IlceAdi = reader.IsDBNull(reader.GetOrdinal("IlceAdi")) ? null : reader.GetString(reader.GetOrdinal("IlceAdi")),
                    SahisAdres = reader.IsDBNull(reader.GetOrdinal("SahisAdres")) ? null : reader.GetString(reader.GetOrdinal("SahisAdres")),
                    Telefon = reader.IsDBNull(reader.GetOrdinal("Telefon")) ? null : reader.GetString(reader.GetOrdinal("Telefon"))
                };
            }

            var urunler = new List<SiparisDetayUrunDto>();
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    urunler.Add(new SiparisDetayUrunDto
                    {
                        SatisId = reader.GetInt32(reader.GetOrdinal("SatisId")),
                        UrunId = reader.IsDBNull(reader.GetOrdinal("UrunId")) ? null : reader.GetInt32(reader.GetOrdinal("UrunId")),
                        UrunKodu = reader.IsDBNull(reader.GetOrdinal("UrunKodu")) ? null : reader.GetString(reader.GetOrdinal("UrunKodu")),
                        UrunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? null : reader.GetString(reader.GetOrdinal("UrunAdi")),
                        KdvOrani = reader.IsDBNull(reader.GetOrdinal("KdvOrani")) ? null : reader.GetInt32(reader.GetOrdinal("KdvOrani")),
                        BirimId = reader.IsDBNull(reader.GetOrdinal("BirimId")) ? null : reader.GetInt32(reader.GetOrdinal("BirimId")),
                        BirimAdi = reader.IsDBNull(reader.GetOrdinal("BirimAdi")) ? null : reader.GetString(reader.GetOrdinal("BirimAdi")),
                        ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                        BirimFiyat = reader.IsDBNull(reader.GetOrdinal("BirimFiyat")) ? null : reader.GetDecimal(reader.GetOrdinal("BirimFiyat")),
                        Miktar = reader.IsDBNull(reader.GetOrdinal("Miktar")) ? null : reader.GetDecimal(reader.GetOrdinal("Miktar")),
                        Toplam = reader.IsDBNull(reader.GetOrdinal("Toplam")) ? null : reader.GetDecimal(reader.GetOrdinal("Toplam"))
                    });
                }
            }

            var tutarlar = new List<SiparisDetayTutarDto>();
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    tutarlar.Add(new SiparisDetayTutarDto
                    {
                        DovizKuru = reader.IsDBNull(reader.GetOrdinal("DovizKuru")) ? null : reader.GetDecimal(reader.GetOrdinal("DovizKuru")),
                        Abbr = reader.IsDBNull(reader.GetOrdinal("Abbr")) ? null : reader.GetString(reader.GetOrdinal("Abbr")),
                        ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                        GenelToplam = reader.IsDBNull(reader.GetOrdinal("GenelToplam")) ? null : reader.GetDecimal(reader.GetOrdinal("GenelToplam")),
                        GenelToplamTL = reader.IsDBNull(reader.GetOrdinal("GenelToplamTL")) ? null : reader.GetDecimal(reader.GetOrdinal("GenelToplamTL")),
                        AttrTopTL = reader.IsDBNull(reader.GetOrdinal("AttrTopTL")) ? null : reader.GetString(reader.GetOrdinal("AttrTopTL"))
                    });
                }
            }

            return (siparis, urunler, tutarlar);
        }

        public async Task<CariBilgilerTutarDto?> GetUyeBilgileriAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUyeBilgileri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            int Ord(string name) => reader.GetOrdinal(name);
            int? GetInt32Safe(string col) => !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
            string? GetStringSafe(string col) => !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
            decimal? GetDecimalSafe(string col) => !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

            return new CariBilgilerTutarDto
            {
                UyeId = !reader.IsDBNull(Ord("UyeId")) ? reader.GetInt32(Ord("UyeId")) : 0,
                Unvan = GetStringSafe("Unvan"),
                VergiNo = GetStringSafe("VergiNo"),
                VergiDairesi = GetStringSafe("VergiDairesi"),
                Eposta = GetStringSafe("Eposta"),
                Telefon = GetStringSafe("Telefon"),
                CurrencyId = GetInt32Safe("CurrencyId"),
                ParaBirimi = GetStringSafe("ParaBirimi"),
                Borc = GetDecimalSafe("Borc"),
                Alacak = GetDecimalSafe("Alacak"),
                Bakiye = GetDecimalSafe("Bakiye"),
                BakiyeTL = GetDecimalSafe("BakiyeTL"),
                VadeGecenGun = GetDecimalSafe("VadeGecenGun"),
                VadesiGecenTutar = GetDecimalSafe("VadesiGecenTutar"),
                VadesiGecenTL = GetDecimalSafe("VadesiGecenTL"),
                VadesiGecenTopTL = GetDecimalSafe("VadesiGecenTopTL"),
                BakiyeTopTL = GetDecimalSafe("BakiyeTopTL")
            };
        }

        public async Task<int> InsertUyeBasvuruAsync(
            byte[] dosya,
            string dosyaAdi,
            string firmaAdi,
            string firmaYetkili,
            string email,
            string sifre,
            string telefon,
            string adres,
            string vergiDairesi,
            string vergiNo,
            int sehirId,
            int ilceId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsUyeBasvuru";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@Dosya", SqlDbType.VarBinary, -1) { Value = dosya ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@DosyaAdi", SqlDbType.VarChar, -1) { Value = dosyaAdi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FirmaAdi", SqlDbType.VarChar, -1) { Value = firmaAdi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@firmaYetkili", SqlDbType.VarChar, -1) { Value = firmaYetkili ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, -1) { Value = email ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Sifre", SqlDbType.VarChar, -1) { Value = sifre ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Telefon", SqlDbType.VarChar, -1) { Value = telefon ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Adres", SqlDbType.VarChar, -1) { Value = adres ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@VergiDairesi", SqlDbType.VarChar, -1) { Value = vergiDairesi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@VergiNo", SqlDbType.VarChar, -1) { Value = vergiNo ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SehirId", SqlDbType.Int) { Value = sehirId });
            cmd.Parameters.Add(new SqlParameter("@IlceId", SqlDbType.Int) { Value = ilceId });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt32(reader.GetOrdinal("val"));
            }
            return 0;
        }

        public async Task<int> UpdateUyeSifreAsync(int uyeId, string vKod, string sifre)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdUyeSifre";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@VKod", SqlDbType.VarChar, -1) { Value = vKod ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Sifre", SqlDbType.VarChar, -1) { Value = sifre ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt32(reader.GetOrdinal("val"));
            }
            return 0;
        }

        public async Task<ParolaYenilemeKoduDto?> GetParolaYenilemeKoduAsync(string eposta)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_MailGetSifreSifirlama";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@EPosta", SqlDbType.VarChar, 500) { Value = eposta ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var uyeId = reader.GetInt32(reader.GetOrdinal("UyeId"));
                var verifyLink = reader.IsDBNull(reader.GetOrdinal("VerifyLink")) ? null : reader.GetString(reader.GetOrdinal("VerifyLink"));
                
                // Extract ParolaYenilemeKodu from VerifyLink
                // Format: domain/Sifre-Belirle/{UyeId}/{ParolaYenilemeKodu}
                string? parolaYenilemeKodu = null;
                if (!string.IsNullOrEmpty(verifyLink))
                {
                    var parts = verifyLink.Split('/');
                    if (parts.Length > 0)
                    {
                        parolaYenilemeKodu = parts[^1]; // Get last segment (ParolaYenilemeKodu)
                    }
                }

                return new ParolaYenilemeKoduDto
                {
                    UyeId = uyeId,
                    ParolaYenilemeKodu = parolaYenilemeKodu
                };
            }
            return null;
        }

        public async Task UpdateSirketLogoAsync(int uyeId, string path)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdSirketLogo";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@Path", SqlDbType.VarChar, -1) { Value = path ?? (object)DBNull.Value });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<CariOdemeDto>> GetOdemelerimAsync(int uyeId, DateTime dateBas, DateTime dateSon)
        {
            var list = new List<CariOdemeDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetOdemelerim";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@DateBas", SqlDbType.Date) { Value = dateBas });
            cmd.Parameters.Add(new SqlParameter("@DateSon", SqlDbType.Date) { Value = dateSon });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CariOdemeDto
                {
                    UyeId = !reader.IsDBNull(reader.GetOrdinal("UyeId")) ? reader.GetInt32(reader.GetOrdinal("UyeId")) : 0,
                    ParaHareketiId = !reader.IsDBNull(reader.GetOrdinal("ParaHareketiId")) ? reader.GetInt32(reader.GetOrdinal("ParaHareketiId")) : null,
                    HareketTarihi = !reader.IsDBNull(reader.GetOrdinal("HareketTarihi")) ? reader.GetDateTime(reader.GetOrdinal("HareketTarihi")) : null,
                    BankaAdi = !reader.IsDBNull(reader.GetOrdinal("BankaAdi")) ? reader.GetString(reader.GetOrdinal("BankaAdi")) : null,
                    KartSahibi = !reader.IsDBNull(reader.GetOrdinal("KartSahibi")) ? reader.GetString(reader.GetOrdinal("KartSahibi")) : null,
                    KartNo = !reader.IsDBNull(reader.GetOrdinal("KartNo")) ? reader.GetString(reader.GetOrdinal("KartNo")) : null,
                    MusteriTemsilcisi = !reader.IsDBNull(reader.GetOrdinal("MusteriTemsilcisi")) ? reader.GetString(reader.GetOrdinal("MusteriTemsilcisi")) : null,
                    OdemeTipi = !reader.IsDBNull(reader.GetOrdinal("OdemeTipi")) ? reader.GetString(reader.GetOrdinal("OdemeTipi")) : null,
                    ParaBirimi = !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    Taksit = !reader.IsDBNull(reader.GetOrdinal("Taksit")) ? reader.GetInt32(reader.GetOrdinal("Taksit")) : null,
                    Tutar = !reader.IsDBNull(reader.GetOrdinal("Tutar")) ? reader.GetDecimal(reader.GetOrdinal("Tutar")) : null,
                    TutarTL = !reader.IsDBNull(reader.GetOrdinal("TutarTL")) ? reader.GetDecimal(reader.GetOrdinal("TutarTL")) : null
                });
            }
            return list;
        }

        public async Task<CariOdemeDetayDto?> GetOdemeDetayAsync(int paraHareketiId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetOdemeDetay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParaHareketiId", SqlDbType.Int) { Value = paraHareketiId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CariOdemeDetayDto
                {
                    Unvan = !reader.IsDBNull(reader.GetOrdinal("Unvan")) ? reader.GetString(reader.GetOrdinal("Unvan")) : null,
                    UyeParaBirimi = !reader.IsDBNull(reader.GetOrdinal("UyeParaBirimi")) ? reader.GetString(reader.GetOrdinal("UyeParaBirimi")) : null,
                    ParaHareketiId = !reader.IsDBNull(reader.GetOrdinal("ParaHareketiId")) ? reader.GetInt32(reader.GetOrdinal("ParaHareketiId")) : null,
                    HareketTarihi = !reader.IsDBNull(reader.GetOrdinal("HareketTarihi")) ? reader.GetDateTime(reader.GetOrdinal("HareketTarihi")) : null,
                    BankaAdi = !reader.IsDBNull(reader.GetOrdinal("BankaAdi")) ? reader.GetString(reader.GetOrdinal("BankaAdi")) : null,
                    KartSahibi = !reader.IsDBNull(reader.GetOrdinal("KartSahibi")) ? reader.GetString(reader.GetOrdinal("KartSahibi")) : null,
                    KartNo = !reader.IsDBNull(reader.GetOrdinal("KartNo")) ? reader.GetString(reader.GetOrdinal("KartNo")) : null,
                    MusteriTemsilcisi = !reader.IsDBNull(reader.GetOrdinal("MusteriTemsilcisi")) ? reader.GetString(reader.GetOrdinal("MusteriTemsilcisi")) : null,
                    OdemeTipi = !reader.IsDBNull(reader.GetOrdinal("OdemeTipi")) ? reader.GetString(reader.GetOrdinal("OdemeTipi")) : null,
                    ParaBirimi = !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    OdemePB = !reader.IsDBNull(reader.GetOrdinal("OdemePB")) ? reader.GetString(reader.GetOrdinal("OdemePB")) : null,
                    Taksit = !reader.IsDBNull(reader.GetOrdinal("Taksit")) ? reader.GetInt32(reader.GetOrdinal("Taksit")) : null,
                    IPAdres = !reader.IsDBNull(reader.GetOrdinal("IPAdres")) ? reader.GetString(reader.GetOrdinal("IPAdres")) : null,
                    DovizKuru = !reader.IsDBNull(reader.GetOrdinal("DovizKuru")) ? reader.GetDecimal(reader.GetOrdinal("DovizKuru")) : null,
                    KomisyonTutar = !reader.IsDBNull(reader.GetOrdinal("KomisyonTutar")) ? reader.GetDecimal(reader.GetOrdinal("KomisyonTutar")) : null,
                    Tutar = !reader.IsDBNull(reader.GetOrdinal("Tutar")) ? reader.GetDecimal(reader.GetOrdinal("Tutar")) : null,
                    TutarTL = !reader.IsDBNull(reader.GetOrdinal("TutarTL")) ? reader.GetDecimal(reader.GetOrdinal("TutarTL")) : null,
                    KartBilgileri = !reader.IsDBNull(reader.GetOrdinal("KartBilgileri")) ? reader.GetString(reader.GetOrdinal("KartBilgileri")) : null,
                    TaksitTutar = !reader.IsDBNull(reader.GetOrdinal("TaksitTutar")) ? reader.GetString(reader.GetOrdinal("TaksitTutar")) : null,
                    FirmaMail = !reader.IsDBNull(reader.GetOrdinal("FirmaMail")) ? reader.GetString(reader.GetOrdinal("FirmaMail")) : null,
                    FirmaName = !reader.IsDBNull(reader.GetOrdinal("FirmaName")) ? reader.GetString(reader.GetOrdinal("FirmaName")) : null,
                    FirmaLogo = !reader.IsDBNull(reader.GetOrdinal("FirmaLogo")) ? reader.GetString(reader.GetOrdinal("FirmaLogo")) : null,
                    FirmaDomain = !reader.IsDBNull(reader.GetOrdinal("FirmaDomain")) ? reader.GetString(reader.GetOrdinal("FirmaDomain")) : null,
                    FirmaAdres = !reader.IsDBNull(reader.GetOrdinal("FirmaAdres")) ? reader.GetString(reader.GetOrdinal("FirmaAdres")) : null,
                    FirmaTel = !reader.IsDBNull(reader.GetOrdinal("FirmaTel")) ? reader.GetString(reader.GetOrdinal("FirmaTel")) : null
                };
            }
            return null;
        }
    }
}
