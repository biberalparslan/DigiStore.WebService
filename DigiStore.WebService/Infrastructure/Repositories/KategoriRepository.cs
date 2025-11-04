using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class KategoriRepository
    {
        private readonly ApplicationDbContext _db;

        public KategoriRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<KategoriDto>> GetKategoriAsync(string dil, int anaKategoriId)
        {
            var result = new List<KategoriDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetKategori";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Dil", SqlDbType.VarChar, 20) { Value = dil });
            cmd.Parameters.Add(new SqlParameter("@AnaKategoriId", SqlDbType.Int) { Value = anaKategoriId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new KategoriDto
                {
                    RN = reader.IsDBNull(reader.GetOrdinal("RN")) ? 0 : reader.GetInt64(reader.GetOrdinal("RN")),
                    KategoriId = reader.GetInt32(reader.GetOrdinal("KategoriId")),
                    AnaKategoriId = reader.GetInt32(reader.GetOrdinal("AnaKategoriId")),
                    Resim = reader.IsDBNull(reader.GetOrdinal("Resim")) ? null : reader.GetString(reader.GetOrdinal("Resim")),
                    KategoriAdet = reader.IsDBNull(reader.GetOrdinal("KategoriAdet")) ? null : reader.GetInt32(reader.GetOrdinal("KategoriAdet")),
                    KategoriAdi = reader.IsDBNull(reader.GetOrdinal("KategoriAdi")) ? null : reader.GetString(reader.GetOrdinal("KategoriAdi"))
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<List<PopulerKategoriDto>> GetPopulerKategoriAsync()
        {
            var result = new List<PopulerKategoriDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetPopulerKategori";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new PopulerKategoriDto
                {
                    RN = reader.IsDBNull(reader.GetOrdinal("RN")) ? null : reader.GetInt64(reader.GetOrdinal("RN")),
                    UrunSayisi = reader.IsDBNull(reader.GetOrdinal("UrunSayisi")) ? null : reader.GetInt32(reader.GetOrdinal("UrunSayisi")),
                    KategoriId = reader.IsDBNull(reader.GetOrdinal("KategoriId")) ? null : reader.GetInt32(reader.GetOrdinal("KategoriId")),
                    KategoriAdi = reader.IsDBNull(reader.GetOrdinal("KategoriAdi")) ? null : reader.GetString(reader.GetOrdinal("KategoriAdi")),
                    Resim = reader.IsDBNull(reader.GetOrdinal("Resim")) ? null : reader.GetString(reader.GetOrdinal("Resim")),
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<List<Dictionary<string, object?>>> GetBreadcrumbAsync(int anaKategoriId, int kategoriId)
        {
            var result = new List<Dictionary<string, object?>>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetBreadcrumb";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AnaKategoriId", SqlDbType.Int) { Value = anaKategoriId });
            cmd.Parameters.Add(new SqlParameter("@KategoriId", SqlDbType.Int) { Value = kategoriId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    row[name] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                }
                result.Add(row);
            }
            return result;
        }
    }
}
