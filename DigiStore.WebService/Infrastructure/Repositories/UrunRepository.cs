using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;
using System.Data.Common;
using System;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class UrunRepository
    {
        private readonly ApplicationDbContext _db;

        public UrunRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        private async Task<List<UrunFirsatDto>> ReadProductsToFirsatUrunDto(DbCommand cmd)
        {
            var result = new List<UrunFirsatDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new UrunFirsatDto
                {
                    UrunId = reader.GetOrdinal("UrunId") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UrunId")) ? reader.GetInt32(reader.GetOrdinal("UrunId")) : 0,
                    UrunAdi = reader.GetOrdinal("UrunAdi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? reader.GetString(reader.GetOrdinal("UrunAdi")) : null,
                    KdvOrani = reader.GetOrdinal("KdvOrani") >= 0 && !reader.IsDBNull(reader.GetOrdinal("KdvOrani")) ? reader.GetInt32(reader.GetOrdinal("KdvOrani")) : null,
                    OrtancaResimAdi = reader.GetOrdinal("OrtancaResimAdi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("OrtancaResimAdi")) ? reader.GetString(reader.GetOrdinal("OrtancaResimAdi")) : null,
                    OrtancaResimAdi2 = reader.GetOrdinal("OrtancaResimAdi2") >= 0 && !reader.IsDBNull(reader.GetOrdinal("OrtancaResimAdi2")) ? reader.GetString(reader.GetOrdinal("OrtancaResimAdi2")) : null,
                    NFiyat = reader.GetOrdinal("NFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("NFiyat")) ? reader.GetDecimal(reader.GetOrdinal("NFiyat")) : null,
                    NEskiFiyat = reader.GetOrdinal("NEskiFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("NEskiFiyat")) ? reader.GetDecimal(reader.GetOrdinal("NEskiFiyat")) : null,
                    ParaBirimi = reader.GetOrdinal("ParaBirimi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    CurrencyId = reader.GetOrdinal("CurrencyId") >= 0 && !reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? reader.GetInt32(reader.GetOrdinal("CurrencyId")) : null,
                    StokMiktari = reader.GetOrdinal("StokMiktari") >= 0 && !reader.IsDBNull(reader.GetOrdinal("StokMiktari")) ? reader.GetInt32(reader.GetOrdinal("StokMiktari")) : null,
                    ToptanFiyat = reader.GetOrdinal("ToptanFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ToptanFiyat")) ? reader.GetDecimal(reader.GetOrdinal("ToptanFiyat")) : null,
                    IndirimliFiyat = reader.GetOrdinal("IndirimliFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("IndirimliFiyat")) ? reader.GetDecimal(reader.GetOrdinal("IndirimliFiyat")) : null,
                    Fiyat = reader.GetOrdinal("Fiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Fiyat")) ? reader.GetDecimal(reader.GetOrdinal("Fiyat")) : null,
                    IndirimOrani = reader.GetOrdinal("IndirimOrani") >= 0 && !reader.IsDBNull(reader.GetOrdinal("IndirimOrani")) ? reader.GetString(reader.GetOrdinal("IndirimOrani")) : null,
                    ClassIndirimOrani = reader.GetOrdinal("ClassIndirimOrani") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ClassIndirimOrani")) ? reader.GetString(reader.GetOrdinal("ClassIndirimOrani")) : null,
                    FiyatFormatted = reader.GetOrdinal("Fiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Fiyat")) ? reader.GetValue(reader.GetOrdinal("Fiyat")).ToString() : null,
                    EskiFiyat = reader.GetOrdinal("EskiFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("EskiFiyat")) ? reader.GetValue(reader.GetOrdinal("EskiFiyat")).ToString() : null,
                    FiyatTL = reader.GetOrdinal("FiyatTL") >= 0 && !reader.IsDBNull(reader.GetOrdinal("FiyatTL")) ? reader.GetValue(reader.GetOrdinal("FiyatTL")).ToString() : null
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunByMarkaDto
        private async Task<List<UrunByMarkaDto>> ReadProductsByMarka(DbCommand cmd)
        {
            var result = new List<UrunByMarkaDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long? GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : (long?)null;
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunByMarkaDto
                {
                    RN = GetInt64Safe("RN"),
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunByKategoriDto
        private async Task<List<UrunByKategoriDto>> ReadProductsByKategori(DbCommand cmd)
        {
            var result = new List<UrunByKategoriDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunByKategoriDto
                {
                    UrunId = Has("UrunId") && !reader.IsDBNull(Ord("UrunId")) ? reader.GetInt32(Ord("UrunId")) : 0,
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    ClassSpecs = GetStringSafe("ClassSpecs"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunByAramaDto
        private async Task<List<UrunByAramaDto>> ReadProductsByArama(DbCommand cmd)
        {
            var result = new List<UrunByAramaDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunByAramaDto
                {
                    UrunId = Has("UrunId") && !reader.IsDBNull(Ord("UrunId")) ? reader.GetInt32(Ord("UrunId")) : 0,
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunByAnaKategoriDto
        private async Task<List<UrunByAnaKategoriDto>> ReadProductsByAnaKategori(DbCommand cmd)
        {
            var result = new List<UrunByAnaKategoriDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long? GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : (long?)null;
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunByAnaKategoriDto
                {
                    RN = GetInt64Safe("RN"),
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunCokSatilanDto
        private async Task<List<UrunCokSatilanDto>> ReadProductsToUrunCokSatilanDto(DbCommand cmd)
        {
            var result = new List<UrunCokSatilanDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : 0;
                int GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : 0;
                int? GetInt32NullableSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunCokSatilanDto
                {
                    RN = GetInt64Safe("RN"),
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32NullableSafe("KdvOrani"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    StokMiktari = GetInt32NullableSafe("StokMiktari"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunIlgiliDto
        private async Task<List<UrunIlgiliDto>> ReadProductsToUrunIlgiliDto(DbCommand cmd)
        {
            var result = new List<UrunIlgiliDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : 0;
                int GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : 0;
                int? GetInt32NullableSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunIlgiliDto
                {
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32NullableSafe("KdvOrani"),
                    MarkaId = GetInt32NullableSafe("MarkaId"),
                    KategoriId = GetInt32NullableSafe("KategoriId"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32NullableSafe("StokMiktari"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    RN = GetInt64Safe("RN"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunIndirimliDto
        private async Task<List<UrunIndirimliDto>> ReadProductsToUrunIndirimliDto(DbCommand cmd)
        {
            var result = new List<UrunIndirimliDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : 0;
                int GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : 0;
                int? GetInt32NullableSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunIndirimliDto
                {
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32NullableSafe("KdvOrani"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32NullableSafe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    RN = GetInt64Safe("RN"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunSonEklenenDto
        private async Task<List<UrunSonEklenenDto>> ReadProductsToUrunSonEklenenDto(DbCommand cmd)
        {
            var result = new List<UrunSonEklenenDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : 0;
                int GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : 0;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var dto = new UrunSonEklenenDto
                {
                    RN = GetInt64Safe("RN"),
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for UrunSonGezilenDto
        private async Task<List<UrunSonGezilenDto>> ReadProductsToUrunSonGezilenDto(DbCommand cmd)
        {
            var result = new List<UrunSonGezilenDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : 0;
                int GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : 0;
                int? GetInt32NullableSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;
                DateTime? GetDateTimeSafe(string col) => Has(col.ToString()) && !reader.IsDBNull(Ord(col.ToString())) ? reader.GetDateTime(Ord(col.ToString())) : (DateTime?)null;

                var dto = new UrunSonGezilenDto
                {
                    UrunId = GetInt32Safe("UrunId"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32NullableSafe("KdvOrani"),
                    GirisTarihi = GetDateTimeSafe("GirisTarihi"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32NullableSafe("StokMiktari"),
                    OrtancaResimAdi = GetStringSafe("OrtancaResimAdi"),
                    OrtancaResimAdi2 = GetStringSafe("OrtancaResimAdi2"),
                    RN = GetInt64Safe("RN"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        // Reader for TumUrunlerDto
        private async Task<List<TumUrunlerDto>> ReadProductsAll(DbCommand cmd)
        {
            const string baseUrl = "https://api.mabelguvenlik.com/urunler/";

            var result = new List<TumUrunlerDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                var resim1 = GetStringSafe("OrtancaResimAdi");
                var resim2 = GetStringSafe("OrtancaResimAdi2");

                // Add base URL if Resim is not null and doesn't already start with http
                if (!string.IsNullOrEmpty(resim1) && !resim1.StartsWith("http", System.StringComparison.OrdinalIgnoreCase))
                {
                    resim1 = $"{baseUrl}{resim1}";
                }
                if (!string.IsNullOrEmpty(resim2) && !resim2.StartsWith("http", System.StringComparison.OrdinalIgnoreCase))
                {
                    resim2 = $"{baseUrl}{resim2}";
                }

                var dto = new TumUrunlerDto
                {
                    UrunId = Has("UrunId") && !reader.IsDBNull(Ord("UrunId")) ? reader.GetInt32(Ord("UrunId")) : 0,
                    UrunAdi = GetStringSafe("UrunAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    OrtancaResimAdi = resim1,
                    OrtancaResimAdi2 = resim2,
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetDecimalSafe("Fiyat"),
                    EskiFiyat = GetDecimalSafe("EskiFiyat")
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<List<UrunByMarkaDto>> GetUrunByMarkaAsync(int markaId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunByMarka";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MarkaId", SqlDbType.Int) { Value = markaId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsByMarka(cmd);
        }

        public async Task<List<UrunByKategoriDto>> GetUrunByKategoriAsync(int kategoriId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunByKategori";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@KategoriId", SqlDbType.Int) { Value = kategoriId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsByKategori(cmd);
        }

        public async Task<List<UrunByAramaDto>> GetUrunByAramaAsync(string query, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunByArama";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@QueryString", SqlDbType.NVarChar) { Value = query });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsByArama(cmd);
        }

        public async Task<List<UrunByAnaKategoriDto>> GetUrunByAnaKategoriAsync(int anaKategoriId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunByAnaKategori";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AnaKategoriId", SqlDbType.Int) { Value = anaKategoriId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsByAnaKategori(cmd);
        }

        public async Task<List<UrunCokSatilanDto>> GetCokSatilanlarAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetCokSatilanlar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsToUrunCokSatilanDto(cmd);
        }

        public async Task<List<UrunFirsatDto>> GetFirsatUrunleriAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetFirsatUrunleri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsToFirsatUrunDto(cmd);
        }

        public async Task<List<UrunIlgiliDto>> GetIlgiliUrunlerAsync(int uyeId, int markaId, int kategoriId, int urunId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetIlgiliUrunler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@MarkaId", SqlDbType.Int) { Value = markaId });
            cmd.Parameters.Add(new SqlParameter("@KategoriId", SqlDbType.Int) { Value = kategoriId });
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            return await ReadProductsToUrunIlgiliDto(cmd);
        }

        public async Task<List<UrunIndirimliDto>> GetIndirimliUrunlerAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetIndirimliUrunler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsToUrunIndirimliDto(cmd);
        }

        public async Task<List<UrunSonEklenenDto>> GetSonEklenenlerAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSonEklenenler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsToUrunSonEklenenDto(cmd);
        }

        public async Task<List<UrunSonGezilenDto>> GetSonGezilenlerAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSonGezilenler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            return await ReadProductsToUrunSonGezilenDto(cmd);
        }

        public async Task<List<UrunGarantiDto>> GetGarantiAsync(int uyeId, string barkod)
        {
            var list = new List<UrunGarantiDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GarantiSorgula";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@Barkod", SqlDbType.VarChar, 100) { Value = barkod });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new UrunGarantiDto
                {
                    Tip = reader.GetOrdinal("Tip") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Tip")) ? reader.GetInt32(reader.GetOrdinal("Tip")) : (int?)null,
                    UrunAdi = reader.GetOrdinal("UrunAdi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? reader.GetString(reader.GetOrdinal("UrunAdi")) : null,
                    GarantiBaslangic = reader.GetOrdinal("GarantiBaslangic") >= 0 && !reader.IsDBNull(reader.GetOrdinal("GarantiBaslangic")) ? reader.GetDateTime(reader.GetOrdinal("GarantiBaslangic")) : (DateTime?)null,
                    GarantiBitis = reader.GetOrdinal("GarantiBitis") >= 0 && !reader.IsDBNull(reader.GetOrdinal("GarantiBitis")) ? reader.GetDateTime(reader.GetOrdinal("GarantiBitis")) : (DateTime?)null,
                    GarantiSuresi = reader.GetOrdinal("GarantiSuresi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("GarantiSuresi")) ? reader.GetString(reader.GetOrdinal("GarantiSuresi")) : null
                };
                list.Add(dto);
            }

            return list;
        }

        public async Task<List<UrunSearchBarDto>> GetUrunBySearchBarAsync(string query, int uyeId)
        {
            var list = new List<UrunSearchBarDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunBySearchBar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@QueryString", SqlDbType.VarChar) { Value = query });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            // Ýlk sonuç setini atla
            if (!await reader.NextResultAsync())
                return list;

            // Ýkinci sonuç setini oku
            // Kolon adlarýný küçük harfe çevirerek dizin tablosu hazýrla
            var ord = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++) ord[reader.GetName(i)] = i;

            int Ord(string name)
            {
                if (ord.TryGetValue(name, out var idx)) return idx;
                // Olasý farklý kolon adlarý için alternatifler
                return name switch
                {
                    "name" => ord.TryGetValue("Name", out var idx2) ? idx2 : -1,
                    "resim" => ord.TryGetValue("Resim", out var idx3) ? idx3 : -1,
                    _ => -1
                };
            }

            while (await reader.ReadAsync())
            {
                var dto = new UrunSearchBarDto
                {
                    name = Ord("name") >= 0 && !reader.IsDBNull(Ord("name")) ? reader.GetString(Ord("name")) : null,
                    UrunId = Ord("UrunId") >= 0 && !reader.IsDBNull(Ord("UrunId")) ? reader.GetInt32(Ord("UrunId")) : (int?)null,
                    KategoriId = Ord("KategoriId") >= 0 && !reader.IsDBNull(Ord("KategoriId")) ? reader.GetInt32(Ord("KategoriId")) : (int?)null,
                    MarkaAdi = Ord("MarkaAdi") >= 0 && !reader.IsDBNull(Ord("MarkaAdi")) ? reader.GetString(Ord("MarkaAdi")) : null,
                    KategoriAdi = Ord("KategoriAdi") >= 0 && !reader.IsDBNull(Ord("KategoriAdi")) ? reader.GetString(Ord("KategoriAdi")) : null,
                    resim = Ord("resim") >= 0 && !reader.IsDBNull(Ord("resim")) ? reader.GetString(Ord("resim")) : null
                };
                list.Add(dto);
            }

            return list;
        }

        public async Task<(UrunDetayDto? detay, List<UrunDetayResimDto> resimler, List<UrunOzellikDto> ozellikler)> GetUrunDetayAsync(int urunId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetUrunDetay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();

            UrunDetayDto? detay = null;
            if (await reader.ReadAsync())
            {
                int Ord(string name) => reader.GetOrdinal(name);
                bool Has(string name) { try { return Ord(name) >= 0; } catch { return false; } }
                long? GetInt64Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? Convert.ToInt64(reader.GetValue(Ord(col))) : (long?)null;
                int? GetInt32Safe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetInt32(Ord(col)) : (int?)null;
                string? GetStringSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetString(Ord(col)) : null;
                decimal? GetDecimalSafe(string col) => Has(col) && !reader.IsDBNull(Ord(col)) ? reader.GetDecimal(Ord(col)) : (decimal?)null;

                detay = new UrunDetayDto
                {
                    UrunId = Has("UrunId") && !reader.IsDBNull(Ord("UrunId")) ? reader.GetInt32(Ord("UrunId")) : 0,
                    AnaKategoriId = GetInt32Safe("AnaKategoriId"),
                    AnaKategoriAdi = GetStringSafe("AnaKategoriAdi"),
                    KategoriId = GetInt32Safe("KategoriId"),
                    KategoriAdi = GetStringSafe("KategoriAdi"),
                    UrunAdi = GetStringSafe("UrunAdi"),
                    MarkaId = GetInt32Safe("MarkaId"),
                    MarkaAdi = GetStringSafe("MarkaAdi"),
                    KdvOrani = GetInt32Safe("KdvOrani"),
                    UrunDetaylari = GetStringSafe("UrunDetaylari"),
                    Tanimlama = GetStringSafe("Tanimlama"),
                    AnahtarKelime = GetStringSafe("AnahtarKelime"),
                    NFiyat = GetDecimalSafe("NFiyat"),
                    NEskiFiyat = GetDecimalSafe("NEskiFiyat"),
                    ParaBirimi = GetStringSafe("ParaBirimi"),
                    StokMiktari = GetInt32Safe("StokMiktari"),
                    StokMiktariPlus = GetStringSafe("StokMiktariPlus"),
                    ClassStock = GetStringSafe("ClassStock"),
                    ClassStokYok = GetStringSafe("ClassStokYok"),
                    ClassStokSorunuz = GetStringSafe("ClassStokSorunuz"),
                    RN = GetInt64Safe("RN"),
                    IndirimOrani = GetStringSafe("IndirimOrani"),
                    IndirimliMi = GetStringSafe("IndirimliMi"),
                    ClassIndirimOrani = GetStringSafe("ClassIndirimOrani"),
                    Fiyat = GetStringSafe("Fiyat"),
                    EskiFiyat = GetStringSafe("EskiFiyat")
                };
            }

            var resimler = new List<UrunDetayResimDto>();
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    resimler.Add(new UrunDetayResimDto
                    {
                        UrunId = !reader.IsDBNull(reader.GetOrdinal("UrunId")) ? reader.GetInt32(reader.GetOrdinal("UrunId")) : 0,
                        OrtancaResimAdi = !reader.IsDBNull(reader.GetOrdinal("OrtancaResimAdi")) ? reader.GetString(reader.GetOrdinal("OrtancaResimAdi")) : null,
                        BuyukResimAdi = !reader.IsDBNull(reader.GetOrdinal("BuyukResimAdi")) ? reader.GetString(reader.GetOrdinal("BuyukResimAdi")) : null
                    });
                }
            }

            var ozellikler = new List<UrunOzellikDto>();
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    ozellikler.Add(new UrunOzellikDto
                    {
                        OzellikAdi = !reader.IsDBNull(reader.GetOrdinal("OzellikAdi")) ? reader.GetString(reader.GetOrdinal("OzellikAdi")) : null,
                        DegerAdi = !reader.IsDBNull(reader.GetOrdinal("DegerAdi")) ? reader.GetString(reader.GetOrdinal("DegerAdi")) : null
                    });
                }
            }

            return (detay, resimler, ozellikler);
        }

        public async Task<List<TumUrunlerDto>> GetTumUrunlerAsync(int? uyeId)
        {
            // If UyeId is null or less than 0, use default value 131397
            int effectiveUyeId = (uyeId.HasValue && uyeId.Value >= 0) ? uyeId.Value : 131397;
            
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                select UrunId, OrtancaResimAdi, Nr
                into #resim from 
                (
                    Select 
                        Isnull(OrtancaResimAdi,'ortanca-'+KucukResimAdi) OrtancaResimAdi,
                        UrunId,
                        ROW_NUMBER() OVER(partition by UrunId Order by UrunResimId ) Nr from UrunResim where Isnull(Profilmi,0)=1
                )t where Nr in (1,2)

                select
                    u.UrunId,
                    u.UrunAdi,
                    u.KdvOrani,
                    u.MarkaId,
                    -1 AnaKategoriId,
                    u.KategoriId,
                    m.Name MarkaAdi,
                    ak.AnaKategoriAdi,
                    k.KategoriAdi,
                    Isnull(re.OrtancaResimAdi,'ortanca_resimsiz.png') OrtancaResimAdi,
                    Isnull(rs.OrtancaResimAdi,re.OrtancaResimAdi) OrtancaResimAdi2,
                    f.NFiyat,
                    f.NEskiFiyat,
                    cr.Symbol ParaBirimi,
                    u.StokMiktari,
                    u.StokMiktariPlus,
                    case when Isnull(u.StokMiktari,0)<5 then 'stock_off' else 'stock_plus' end ClassStock,
                    case when Isnull(u.StokMiktari,0)>0 then '' else 'hidden' end ClassStokYok,
                    case when Isnull(u.StokMiktari,0)>0 then 'hidden' else '' end ClassStokSorunuz
                into #urunler
                from V_B2B_Urun u
                inner join Currency cr on cr.CurrencyId = u.CurrencyId
                left join V_CariUrunFiyat f on f.UrunId = u.UrunId and f.UyeId = @UyeId
                left join #resim re on u.UrunId=re.UrunId and re.Nr=1
                left join #resim rs on u.UrunId=rs.UrunId and rs.Nr=2
                left join Marka m on m.MarkaId=u.MarkaId
                left join AnaKategori ak on ak.AnaKategoriId=u.AnaKategoriId
                left join Kategori k on k.KategoriId=u.KategoriId

                update #urunler set Neskifiyat = null where Neskifiyat = 0

                declare @KDVDahil bit
                declare @B2BKDVDahil bit

                select @KDVDahil = Isnull(KdvDahil,0),@B2BKDVDahil=Isnull(B2BKDVDahil,0) from Ayar

                select
                    *,
                    convert(varchar,'%'+convert(varchar,convert(int,(NEskiFiyat-NFiyat)/NEskiFiyat*100))) IndirimOrani,
                    case when convert(int,(NEskiFiyat-NFiyat)/NEskiFiyat*100) > 0 then '' else 'hidden' end ClassIndirimOrani,
                    convert(decimal(18,2),
                        NFiyat / 
                            case when @KDVDahil = 1 and @B2BKDVDahil = 0 then (1+(KdvOrani/100.0)) else 1 end
                            )
                        Fiyat,
                    convert(decimal(18,2),
                        NEskiFiyat /
                            case when @KDVDahil = 1 and @B2BKDVDahil = 0 then (1+(KdvOrani/100.0)) else 1 end
                            )
                        EskiFiyat
                from #urunler
                order by KategoriId asc";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = effectiveUyeId });
            return await ReadProductsAll(cmd);
        }
    }
}
