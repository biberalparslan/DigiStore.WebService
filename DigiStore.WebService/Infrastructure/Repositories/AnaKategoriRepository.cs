using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class AnaKategoriRepository
    {
        private readonly ApplicationDbContext _db;

        public AnaKategoriRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<AnaKategoriDto>> GetAnaKategoriAsync()
        {
            var result = new List<AnaKategoriDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetAnaKategori";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new AnaKategoriDto
                {
                    RN = reader.IsDBNull(reader.GetOrdinal("RN")) ? 0 : reader.GetInt64(reader.GetOrdinal("RN")),
                    AnaKategoriId = reader.IsDBNull(reader.GetOrdinal("AnaKategoriId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AnaKategoriId")),
                    AnaKategoriAdi = reader.IsDBNull(reader.GetOrdinal("AnaKategoriAdi")) ? null : reader.GetString(reader.GetOrdinal("AnaKategoriAdi")),
                    IsHidden = reader.IsDBNull(reader.GetOrdinal("IsHidden")) ? false : (reader.GetString(reader.GetOrdinal("IsHidden")) == "hidden")
                };
                result.Add(dto);
            }
            return result;
        }
    }
}
