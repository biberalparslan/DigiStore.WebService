using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class SepetRepository
    {
        private readonly ApplicationDbContext _db;

        public SepetRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<SepetDto>> GetSepetimAsync(int uyeId)
        {
            var result = new List<SepetDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSepetim";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new SepetDto
                {
                    UrunId = reader.GetOrdinal("UrunId") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UrunId")) ? reader.GetInt32(reader.GetOrdinal("UrunId")) : 0,
                    KucukResimAdi = reader.GetOrdinal("KucukResimAdi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("KucukResimAdi")) ? reader.GetString(reader.GetOrdinal("KucukResimAdi")) : null,
                    UrunAdi = reader.GetOrdinal("UrunAdi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? reader.GetString(reader.GetOrdinal("UrunAdi")) : null,
                    BirimFiyat = reader.GetOrdinal("BirimFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("BirimFiyat")) ? reader.GetDecimal(reader.GetOrdinal("BirimFiyat")) : null,
                    ToplamFiyat = reader.GetOrdinal("ToplamFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ToplamFiyat")) ? reader.GetDecimal(reader.GetOrdinal("ToplamFiyat")) : null,
                    AraToplam = reader.GetOrdinal("AraToplam") >= 0 && !reader.IsDBNull(reader.GetOrdinal("AraToplam")) ? reader.GetDecimal(reader.GetOrdinal("AraToplam")) : null,
                    KdvOrani = reader.GetOrdinal("KdvOrani") >= 0 && !reader.IsDBNull(reader.GetOrdinal("KdvOrani")) ? reader.GetInt32(reader.GetOrdinal("KdvOrani")) : null,
                    KDV = reader.GetOrdinal("KDV") >= 0 && !reader.IsDBNull(reader.GetOrdinal("KDV")) ? reader.GetDecimal(reader.GetOrdinal("KDV")) : null,
                    ParaBirimi = reader.GetOrdinal("ParaBirimi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    TLFiyat = reader.GetOrdinal("TLFiyat") >= 0 && !reader.IsDBNull(reader.GetOrdinal("TLFiyat")) ? reader.GetDecimal(reader.GetOrdinal("TLFiyat")) : null,
                    Adet = reader.GetOrdinal("Adet") >= 0 && !reader.IsDBNull(reader.GetOrdinal("Adet")) ? reader.GetInt32(reader.GetOrdinal("Adet")) : null,
                    StokMiktari = reader.GetOrdinal("StokMiktari") >= 0 && !reader.IsDBNull(reader.GetOrdinal("StokMiktari")) ? reader.GetInt32(reader.GetOrdinal("StokMiktari")) : null,
                    SepetTutar = reader.GetOrdinal("SepetTutar") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SepetTutar")) ? reader.GetDecimal(reader.GetOrdinal("SepetTutar")) : null,
                    SepetAdedi = reader.GetOrdinal("SepetAdedi") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SepetAdedi")) ? reader.GetInt32(reader.GetOrdinal("SepetAdedi")) : null,
                    SepetKDV = reader.GetOrdinal("SepetKDV") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SepetKDV")) ? reader.GetDecimal(reader.GetOrdinal("SepetKDV")) : null,
                    SepetAraToplam = reader.GetOrdinal("SepetAraToplam") >= 0 && !reader.IsDBNull(reader.GetOrdinal("SepetAraToplam")) ? reader.GetDecimal(reader.GetOrdinal("SepetAraToplam")) : null
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<bool> InsertSepetAsync(int uyeId, int urunId, int adet)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsSepet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@Adet", SqlDbType.Int) { Value = adet });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateSepetAdetAsync(int uyeId, int urunId, int adet)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdSepetAdet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            cmd.Parameters.Add(new SqlParameter("@Adet", SqlDbType.Int) { Value = adet });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteSepetAsync(int uyeId, int urunId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_DelSepet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UrunId", SqlDbType.Int) { Value = urunId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<int> InsertSiparisAsync(int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsSiparis";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            
            // Execute and get scalar result
            var result = await cmd.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
