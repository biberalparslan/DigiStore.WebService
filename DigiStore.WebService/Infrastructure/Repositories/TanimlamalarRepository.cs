using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class TanimlamalarRepository
    {
        private readonly ApplicationDbContext _db;
        public TanimlamalarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// B2B_GetFirmaBilgileri SP'ini çaðýrýr ve sonucu FirmaBilgileriDto olarak döner.
        /// </summary>
        public async Task<FirmaBilgileriDto?> GetFirmaBilgileriAsync()
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetFirmaBilgileri";
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            // Sütun adlarýný hýzlý eriþim için sözlüðe al
            var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++) ordinals[reader.GetName(i)] = i;

            var dto = new FirmaBilgileriDto();
            var props = typeof(FirmaBilgileriDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                if (!ordinals.TryGetValue(p.Name, out var idx)) continue;
                if (await reader.IsDBNullAsync(idx)) continue;

                var val = reader.GetValue(idx);
                try
                {
                    var targetType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                    if (targetType.IsEnum)
                    {
                        p.SetValue(dto, Enum.ToObject(targetType, val));
                    }
                    else if (targetType == typeof(bool) && val is string s)
                    {
                        // Bazý alanlar 'True' / 'False' metin olarak dönebilir
                        if (bool.TryParse(s, out var b)) p.SetValue(dto, b);
                    }
                    else
                    {
                        p.SetValue(dto, Convert.ChangeType(val, targetType));
                    }
                }
                catch
                {
                    // Tür dönüþümü yapýlamazsa sessizce geç
                }
            }

            return dto;
        }

        public async Task<List<SehirlerDto>> GetSehirlerAsync()
        {
            var list = new List<SehirlerDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetSehirler";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new SehirlerDto
                {
                    SehirId = reader.IsDBNull(reader.GetOrdinal("SehirId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SehirId")),
                    SehirAdi = reader.IsDBNull(reader.GetOrdinal("SehirAdi")) ? null : reader.GetString(reader.GetOrdinal("SehirAdi"))
                });
            }
            return list;
        }

        public async Task<List<IlcelerDto>> GetIlcelerAsync(int sehirId)
        {
            var list = new List<IlcelerDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetIlceler";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SehirId", SqlDbType.Int) { Value = sehirId });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new IlcelerDto
                {
                    IlceId = reader.IsDBNull(reader.GetOrdinal("ilceId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ilceId")),
                    IlceAdi = reader.IsDBNull(reader.GetOrdinal("IlceAdi")) ? null : reader.GetString(reader.GetOrdinal("IlceAdi"))
                });
            }
            return list;
        }

        public async Task<List<DovizKuruDto>> GetKurBilgileriAsync()
        {
            var list = new List<DovizKuruDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetKurBilgileri";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new DovizKuruDto
                {
                    CurrencyId = !reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? reader.GetInt32(reader.GetOrdinal("CurrencyId")) : 0,
                    Kur = !reader.IsDBNull(reader.GetOrdinal("Kur")) ? reader.GetDecimal(reader.GetOrdinal("Kur")) : (decimal?)null
                });
            }
            return list;
        }

        public async Task<List<MarkalarDto>> GetMarkalarAsync()
        {
            var list = new List<MarkalarDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetMarkalar";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new MarkalarDto
                {
                    MarkaId = !reader.IsDBNull(reader.GetOrdinal("MarkaId")) ? reader.GetInt32(reader.GetOrdinal("MarkaId")) : 0,
                    MarkaAdi = !reader.IsDBNull(reader.GetOrdinal("MarkaAdi")) ? reader.GetString(reader.GetOrdinal("MarkaAdi")) : null,
                    RN = !reader.IsDBNull(reader.GetOrdinal("RN")) ? reader.GetInt64(reader.GetOrdinal("RN")) : (long?)null
                });
            }
            return list;
        }

        public async Task<List<BannerResimDto>> GetBannerResimAsync()
        {
            var list = new List<BannerResimDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetBannerResim";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new BannerResimDto
                {
                    ResimAdi = !reader.IsDBNull(reader.GetOrdinal("ResimAdi")) ? reader.GetString(reader.GetOrdinal("ResimAdi")) : null,
                    Url = !reader.IsDBNull(reader.GetOrdinal("Url")) ? reader.GetString(reader.GetOrdinal("Url")) : null
                });
            }
            return list;
        }

        public async Task<List<VitrinResimDto>> GetVitrinResimAsync()
        {
            var list = new List<VitrinResimDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetVitrinResim";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new VitrinResimDto
                {
                    ResimAdi = !reader.IsDBNull(reader.GetOrdinal("ResimAdi")) ? reader.GetString(reader.GetOrdinal("ResimAdi")) : null,
                    Url = !reader.IsDBNull(reader.GetOrdinal("Url")) ? reader.GetString(reader.GetOrdinal("Url")) : null,
                    UstBaslik = !reader.IsDBNull(reader.GetOrdinal("UstBaslik")) ? reader.GetString(reader.GetOrdinal("UstBaslik")) : null,
                    Baslik = !reader.IsDBNull(reader.GetOrdinal("Baslik")) ? reader.GetString(reader.GetOrdinal("Baslik")) : null,
                    Aciklama = !reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? reader.GetString(reader.GetOrdinal("Aciklama")) : null
                });
            }
            return list;
        }
    }
}
