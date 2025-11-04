using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class AddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<AdresDto>> GetAdreslerAsync(int uyeId)
        {
            var result = new List<AdresDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetAdresler";
            cmd.CommandType = CommandType.StoredProcedure;
            var p = new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId };
            cmd.Parameters.Add(p);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dto = new AdresDto
                {
                    AdresId = reader.GetInt32(reader.GetOrdinal("AdresId")),
                    UyeId = reader.IsDBNull(reader.GetOrdinal("UyeId")) ? null : reader.GetInt32(reader.GetOrdinal("UyeId")),
                    AdresAdi = reader.IsDBNull(reader.GetOrdinal("AdresAdi")) ? null : reader.GetString(reader.GetOrdinal("AdresAdi")),
                    AdSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? null : reader.GetString(reader.GetOrdinal("AdSoyad")),
                    SahisAdres = reader.IsDBNull(reader.GetOrdinal("SahisAdres")) ? null : reader.GetString(reader.GetOrdinal("SahisAdres")),
                    SahisPostaKodu = reader.IsDBNull(reader.GetOrdinal("SahisPostaKodu")) ? null : reader.GetString(reader.GetOrdinal("SahisPostaKodu")),
                    EPosta = reader.IsDBNull(reader.GetOrdinal("EPosta")) ? null : reader.GetString(reader.GetOrdinal("EPosta")),
                    Firma = reader.IsDBNull(reader.GetOrdinal("Firma")) ? null : reader.GetString(reader.GetOrdinal("Firma")),
                    VergiDairesi = reader.IsDBNull(reader.GetOrdinal("VergiDairesi")) ? null : reader.GetString(reader.GetOrdinal("VergiDairesi")),
                    VergiNo = reader.IsDBNull(reader.GetOrdinal("VergiNo")) ? null : reader.GetString(reader.GetOrdinal("VergiNo")),
                    FirmaAdres = reader.IsDBNull(reader.GetOrdinal("FirmaAdres")) ? null : reader.GetString(reader.GetOrdinal("FirmaAdres")),
                    FirmaPostaKodu = reader.IsDBNull(reader.GetOrdinal("FirmaPostaKodu")) ? null : reader.GetString(reader.GetOrdinal("FirmaPostaKodu")),
                    Semt = reader.IsDBNull(reader.GetOrdinal("Semt")) ? null : reader.GetString(reader.GetOrdinal("Semt")),
                    IlceId = reader.IsDBNull(reader.GetOrdinal("IlceId")) ? null : reader.GetInt32(reader.GetOrdinal("IlceId")),
                    SehirId = reader.IsDBNull(reader.GetOrdinal("SehirId")) ? null : reader.GetInt32(reader.GetOrdinal("SehirId")),
                    UlkeId = reader.IsDBNull(reader.GetOrdinal("UlkeId")) ? null : reader.GetInt32(reader.GetOrdinal("UlkeId")),
                    Telefon = reader.IsDBNull(reader.GetOrdinal("Telefon")) ? null : reader.GetString(reader.GetOrdinal("Telefon")),
                    Telefon2 = reader.IsDBNull(reader.GetOrdinal("Telefon2")) ? null : reader.GetString(reader.GetOrdinal("Telefon2")),
                    AdresDetayi = reader.IsDBNull(reader.GetOrdinal("AdresDetayi")) ? null : reader.GetString(reader.GetOrdinal("AdresDetayi")),
                    TeslimatAdresiMi = reader.IsDBNull(reader.GetOrdinal("TeslimatAdresiMi")) ? null : reader.GetBoolean(reader.GetOrdinal("TeslimatAdresiMi")),
                    FaturaAdresiMi = reader.IsDBNull(reader.GetOrdinal("FaturaAdresiMi")) ? null : reader.GetBoolean(reader.GetOrdinal("FaturaAdresiMi")),
                    AnonimUyeId = reader.IsDBNull(reader.GetOrdinal("AnonimUyeId")) ? null : reader.GetString(reader.GetOrdinal("AnonimUyeId")),
                    ModifiedOn = reader.IsDBNull(reader.GetOrdinal("ModifiedOn")) ? null : reader.GetDateTime(reader.GetOrdinal("ModifiedOn")),
                    SehirAdi = reader.IsDBNull(reader.GetOrdinal("SehirAdi")) ? null : reader.GetString(reader.GetOrdinal("SehirAdi")),
                    IlceAdi = reader.IsDBNull(reader.GetOrdinal("IlceAdi")) ? null : reader.GetString(reader.GetOrdinal("IlceAdi")),
                    DefaultMu = reader.IsDBNull(reader.GetOrdinal("DefaultMu")) ? false : (reader.GetString(reader.GetOrdinal("DefaultMu")) == "True")
                };
                result.Add(dto);
            }

            return result;
        }

        public async Task<AdresDto?> GetAdresByIdAsync(int adresId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetAdresById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int) { Value = adresId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var dto = new AdresDto
                {
                    AdresId = reader.GetInt32(reader.GetOrdinal("AdresId")),
                    UyeId = reader.IsDBNull(reader.GetOrdinal("UyeId")) ? null : reader.GetInt32(reader.GetOrdinal("UyeId")),
                    AdresAdi = reader.IsDBNull(reader.GetOrdinal("AdresAdi")) ? null : reader.GetString(reader.GetOrdinal("AdresAdi")),
                    AdSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? null : reader.GetString(reader.GetOrdinal("AdSoyad")),
                    SahisAdres = reader.IsDBNull(reader.GetOrdinal("SahisAdres")) ? null : reader.GetString(reader.GetOrdinal("SahisAdres")),
                    SahisPostaKodu = reader.IsDBNull(reader.GetOrdinal("SahisPostaKodu")) ? null : reader.GetString(reader.GetOrdinal("SahisPostaKodu")),
                    EPosta = reader.IsDBNull(reader.GetOrdinal("EPosta")) ? null : reader.GetString(reader.GetOrdinal("EPosta")),
                    Firma = reader.IsDBNull(reader.GetOrdinal("Firma")) ? null : reader.GetString(reader.GetOrdinal("Firma")),
                    VergiDairesi = reader.IsDBNull(reader.GetOrdinal("VergiDairesi")) ? null : reader.GetString(reader.GetOrdinal("VergiDairesi")),
                    VergiNo = reader.IsDBNull(reader.GetOrdinal("VergiNo")) ? null : reader.GetString(reader.GetOrdinal("VergiNo")),
                    FirmaAdres = reader.IsDBNull(reader.GetOrdinal("FirmaAdres")) ? null : reader.GetString(reader.GetOrdinal("FirmaAdres")),
                    FirmaPostaKodu = reader.IsDBNull(reader.GetOrdinal("FirmaPostaKodu")) ? null : reader.GetString(reader.GetOrdinal("FirmaPostaKodu")),
                    Semt = reader.IsDBNull(reader.GetOrdinal("Semt")) ? null : reader.GetString(reader.GetOrdinal("Semt")),
                    IlceId = reader.IsDBNull(reader.GetOrdinal("IlceId")) ? null : reader.GetInt32(reader.GetOrdinal("IlceId")),
                    SehirId = reader.IsDBNull(reader.GetOrdinal("SehirId")) ? null : reader.GetInt32(reader.GetOrdinal("SehirId")),
                    UlkeId = reader.IsDBNull(reader.GetOrdinal("UlkeId")) ? null : reader.GetInt32(reader.GetOrdinal("UlkeId")),
                    Telefon = reader.IsDBNull(reader.GetOrdinal("Telefon")) ? null : reader.GetString(reader.GetOrdinal("Telefon")),
                    Telefon2 = reader.IsDBNull(reader.GetOrdinal("Telefon2")) ? null : reader.GetString(reader.GetOrdinal("Telefon2")),
                    AdresDetayi = reader.IsDBNull(reader.GetOrdinal("AdresDetayi")) ? null : reader.GetString(reader.GetOrdinal("AdresDetayi")),
                    TeslimatAdresiMi = reader.IsDBNull(reader.GetOrdinal("TeslimatAdresiMi")) ? null : reader.GetBoolean(reader.GetOrdinal("TeslimatAdresiMi")),
                    FaturaAdresiMi = reader.IsDBNull(reader.GetOrdinal("FaturaAdresiMi")) ? null : reader.GetBoolean(reader.GetOrdinal("FaturaAdresiMi")),
                    AnonimUyeId = reader.IsDBNull(reader.GetOrdinal("AnonimUyeId")) ? null : reader.GetString(reader.GetOrdinal("AnonimUyeId")),
                    ModifiedOn = reader.IsDBNull(reader.GetOrdinal("ModifiedOn")) ? null : reader.GetDateTime(reader.GetOrdinal("ModifiedOn"))
                };
                return dto;
            }

            return null;
        }

        public async Task<bool> DelAdresAsync(int adresId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_DelAdres";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int) { Value = adresId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> UpdDefAdresAsync(int adresId, int uyeId)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdDefAdres";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int) { Value = adresId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });

            var scalar = await cmd.ExecuteScalarAsync();
            if (scalar == null) return false;
            if (int.TryParse(scalar.ToString(), out var val)) return val == 1;
            return false;
        }

        public async Task<int> UpdAdresAsync(AdresDto dto)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdAdres";
            cmd.CommandType = CommandType.StoredProcedure;

            var adresIdParam = dto.AdresId == 0 ? -1 : dto.AdresId;
            cmd.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int) { Value = adresIdParam });
            cmd.Parameters.Add(new SqlParameter("@AdresAdi", SqlDbType.VarChar, 300) { Value = dto.AdresAdi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@AdSoyad", SqlDbType.VarChar, 300) { Value = dto.AdSoyad ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Telefon", SqlDbType.VarChar, 300) { Value = dto.Telefon ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@EPosta", SqlDbType.VarChar, 300) { Value = dto.EPosta ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Adres", SqlDbType.NVarChar) { Value = dto.SahisAdres ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FirmaAdi", SqlDbType.VarChar, 300) { Value = dto.Firma ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@VergiDairesi", SqlDbType.VarChar, 300) { Value = dto.VergiDairesi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@VergiNo", SqlDbType.VarChar, 300) { Value = dto.VergiNo ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SehirId", SqlDbType.Int) { Value = dto.SehirId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IlceId", SqlDbType.Int) { Value = dto.IlceId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = dto.UyeId ?? (object)DBNull.Value });

            var scalar = await cmd.ExecuteScalarAsync();
            if (scalar == null) return -1;
            if (int.TryParse(scalar.ToString(), out var val)) return val;
            return -1;
        }
    }
}
