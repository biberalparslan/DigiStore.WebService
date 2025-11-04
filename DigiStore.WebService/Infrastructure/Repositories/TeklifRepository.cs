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
    public class TeklifRepository
    {
        private readonly ApplicationDbContext _db;

        public TeklifRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TeklifDto>> GetTekliflerAsync(int uyeId)
        {
            var list = new List<TeklifDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetTeklifler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new TeklifDto
                {
                    TeklifId = reader.GetInt32(reader.GetOrdinal("TeklifId")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    TeklifAdi = reader.IsDBNull(reader.GetOrdinal("TeklifAdi")) ? null : reader.GetString(reader.GetOrdinal("TeklifAdi")),
                    Oran = reader.IsDBNull(reader.GetOrdinal("Oran")) ? null : reader.GetDecimal(reader.GetOrdinal("Oran")),
                    TeklifTarihi = reader.IsDBNull(reader.GetOrdinal("TeklifTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("TeklifTarihi"))
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<TeklifDto?> GetTeklifDetayAsync(int teklifId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetTeklifDetay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = teklifId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            // Read first result set (#teklif)
            using var reader = await cmd.ExecuteReaderAsync();
            TeklifDto? header = null;
            if (await reader.ReadAsync())
            {
                header = new TeklifDto
                {
                    TeklifId = reader.GetInt32(reader.GetOrdinal("TeklifId")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    TeklifAdi = reader.IsDBNull(reader.GetOrdinal("TeklifAdi")) ? null : reader.GetString(reader.GetOrdinal("TeklifAdi")),
                    Oran = reader.IsDBNull(reader.GetOrdinal("Oran")) ? null : reader.GetDecimal(reader.GetOrdinal("Oran")),
                    Aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? null : reader.GetString(reader.GetOrdinal("Aciklama")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi")),
                    DovizKuru = reader.IsDBNull(reader.GetOrdinal("DovizKuru")) ? null : reader.GetDecimal(reader.GetOrdinal("DovizKuru")),
                    GecerlilikTarihi = reader.IsDBNull(reader.GetOrdinal("GecerlilikTarihi")) ? null : reader.GetDateTime(reader.GetOrdinal("GecerlilikTarihi")),
                    AraToplam = reader.IsDBNull(reader.GetOrdinal("AraToplam")) ? null : reader.GetDecimal(reader.GetOrdinal("AraToplam")),
                    ToplamKDV = reader.IsDBNull(reader.GetOrdinal("ToplamKDV")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamKDV")),
                    ToplamTutar = reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamTutar")),
                    AraToplamTL = reader.IsDBNull(reader.GetOrdinal("AraToplamTL")) ? null : reader.GetDecimal(reader.GetOrdinal("AraToplamTL")),
                    ToplamKDVTL = reader.IsDBNull(reader.GetOrdinal("ToplamKDVTL")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamKDVTL")),
                    ToplamTutarTL = reader.IsDBNull(reader.GetOrdinal("ToplamTutarTL")) ? null : reader.GetDecimal(reader.GetOrdinal("ToplamTutarTL"))
                };
            }

            // Move to next result set (TeklifLines)
            if (!await reader.NextResultAsync()) return header;

            var details = new List<TeklifDetayDto>();
            while (await reader.ReadAsync())
            {
                var d = new TeklifDetayDto
                {
                    TeklifLinesId = reader.GetInt32(reader.GetOrdinal("TeklifLinesId")),
                    TeklifId = reader.GetInt32(reader.GetOrdinal("TeklifId")),
                    UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                    UrunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? null : reader.GetString(reader.GetOrdinal("UrunAdi")),
                    KucukResimAdi = reader.IsDBNull(reader.GetOrdinal("KucukResimAdi")) ? null : reader.GetString(reader.GetOrdinal("KucukResimAdi")),
                    GuncelFiyat = reader.IsDBNull(reader.GetOrdinal("GuncelFiyat")) ? null : reader.GetDecimal(reader.GetOrdinal("GuncelFiyat")),
                    TeklifFiyat = reader.IsDBNull(reader.GetOrdinal("TeklifFiyat")) ? null : reader.GetDecimal(reader.GetOrdinal("TeklifFiyat")),
                    Miktar = reader.IsDBNull(reader.GetOrdinal("Miktar")) ? null : reader.GetInt32(reader.GetOrdinal("Miktar")),
                    Birim = reader.IsDBNull(reader.GetOrdinal("Birim")) ? null : reader.GetString(reader.GetOrdinal("Birim")),
                    BirimId = reader.IsDBNull(reader.GetOrdinal("BirimId")) ? null : reader.GetInt32(reader.GetOrdinal("BirimId")),
                    KDVOrani = reader.IsDBNull(reader.GetOrdinal("KDVOrani")) ? null : reader.GetDecimal(reader.GetOrdinal("KDVOrani")),
                    AlisFiyati = reader.IsDBNull(reader.GetOrdinal("AlisFiyati")) ? null : reader.GetDecimal(reader.GetOrdinal("AlisFiyati")),
                    ParaBirimi = reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? null : reader.GetString(reader.GetOrdinal("ParaBirimi"))
                };
                details.Add(d);
            }

            if (header != null) header.Details = details;
            return header;
        }

        public async Task<string?> GetTeklifDetayHtmlAsync(int teklifId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetTeklifDetay4Html";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = teklifId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.IsDBNull(0) ? null : reader.GetString(0);
            }
            return null;
        }

        public async Task<bool> UpdateTeklifAsync(TeklifDto dto)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdTeklif";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = dto.UyeId });
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = dto.TeklifId });
            cmd.Parameters.Add(new SqlParameter("@TeklifAdi", SqlDbType.VarChar) { Value = dto.TeklifAdi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Oran", SqlDbType.Decimal) { Value = dto.Oran ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@DovizKuru", SqlDbType.Decimal) { Value = dto.DovizKuru ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@GecerlilikTarihi", SqlDbType.Date) { Value = dto.GecerlilikTarihi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Aciklama", SqlDbType.VarChar) { Value = dto.Aciklama ?? (object)DBNull.Value });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateTeklifFiyatAsync(int uyeId, int teklifId, int urunId, decimal birimFiyat)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdTeklifFiyat";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = teklifId });
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            cmd.Parameters.Add(new SqlParameter("@BirimFiyat", SqlDbType.Decimal) { Value = birimFiyat });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteTeklifAsync(int teklifId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_DelTeklif";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = teklifId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<int> InsertSiparisFromTeklifAsync(int teklifId, int uyeId, int teslimatAdresId = -1)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsSiparisFromTeklif";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TeklifId", SqlDbType.Int) { Value = teklifId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@TeslimatAdresId", SqlDbType.Int) { Value = teslimatAdresId });
            
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt32(reader.GetOrdinal("val"));
            }
            return 0;
        }
    }
}
