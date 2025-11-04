using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class OdemeRepository
    {
        private readonly ApplicationDbContext _db;

        public OdemeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OdemeInsertDto?> InsertOdemeAsync(
            int uyeId, 
            decimal odenecekTutar, 
            int krediKarti, 
            int taksit, 
            string kartSahibi, 
            string kartNo, 
            string ip)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsOdeme";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@OdenecekTutar", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = odenecekTutar });
            cmd.Parameters.Add(new SqlParameter("@KrediKarti", SqlDbType.Int) { Value = krediKarti });
            cmd.Parameters.Add(new SqlParameter("@Taksit", SqlDbType.Int) { Value = taksit });
            cmd.Parameters.Add(new SqlParameter("@KartSahibi", SqlDbType.VarChar, 100) { Value = kartSahibi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@KartNo", SqlDbType.VarChar, 50) { Value = kartNo ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Ip", SqlDbType.VarChar, 50) { Value = ip ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OdemeInsertDto
                {
                    SonKullanmaAy = reader.IsDBNull(reader.GetOrdinal("SonKullanmaAy")) ? null : reader.GetString(reader.GetOrdinal("SonKullanmaAy")),
                    SonKullanmaYil = reader.IsDBNull(reader.GetOrdinal("SonKullanmaYil")) ? null : reader.GetString(reader.GetOrdinal("SonKullanmaYil")),
                    CVV = reader.IsDBNull(reader.GetOrdinal("CVV")) ? null : reader.GetString(reader.GetOrdinal("CVV")),
                    OdenecekTutar = reader.GetDecimal(reader.GetOrdinal("OdenecekTutar")),
                    BankaKodu = reader.GetInt32(reader.GetOrdinal("BankaKodu")),
                    NKodPosHesaplari = reader.GetInt32(reader.GetOrdinal("nKodPosHesaplari")),
                    EklenenTaksit = reader.GetInt32(reader.GetOrdinal("EklenenTaksit")),
                    KartSahibi = reader.IsDBNull(reader.GetOrdinal("KartSahibi")) ? null : reader.GetString(reader.GetOrdinal("KartSahibi")),
                    KartNo = reader.IsDBNull(reader.GetOrdinal("KartNo")) ? null : reader.GetString(reader.GetOrdinal("KartNo")),
                    Taksit = reader.GetInt32(reader.GetOrdinal("Taksit")),
                    IPAdres = reader.IsDBNull(reader.GetOrdinal("IPAdres")) ? null : reader.GetString(reader.GetOrdinal("IPAdres")),
                    OdemeId = reader.GetInt32(reader.GetOrdinal("OdemeId")),
                    CariTelefon = reader.IsDBNull(reader.GetOrdinal("CariTelefon")) ? null : reader.GetString(reader.GetOrdinal("CariTelefon")),
                    OdemeKey = reader.IsDBNull(reader.GetOrdinal("OdemeKey")) ? null : reader.GetString(reader.GetOrdinal("OdemeKey")),
                    Domain = reader.IsDBNull(reader.GetOrdinal("Domain")) ? null : reader.GetString(reader.GetOrdinal("Domain")),
                    MailServer = reader.IsDBNull(reader.GetOrdinal("MailServer")) ? null : reader.GetString(reader.GetOrdinal("MailServer")),
                    MailServerPort = reader.IsDBNull(reader.GetOrdinal("MailServerPort")) ? null : reader.GetInt32(reader.GetOrdinal("MailServerPort")),
                    MailUserName = reader.IsDBNull(reader.GetOrdinal("MailUserName")) ? null : reader.GetString(reader.GetOrdinal("MailUserName")),
                    MailPassword = reader.IsDBNull(reader.GetOrdinal("MailPassword")) ? null : reader.GetString(reader.GetOrdinal("MailPassword")),
                    Unvan = reader.IsDBNull(reader.GetOrdinal("Unvan")) ? null : reader.GetString(reader.GetOrdinal("Unvan")),
                    B2CDomain = reader.IsDBNull(reader.GetOrdinal("B2CDomain")) ? null : reader.GetString(reader.GetOrdinal("B2CDomain")),
                    B2CMailServer = reader.IsDBNull(reader.GetOrdinal("B2CMailServer")) ? null : reader.GetString(reader.GetOrdinal("B2CMailServer")),
                    B2CMailServerPort = reader.IsDBNull(reader.GetOrdinal("B2CMailServerPort")) ? null : reader.GetInt32(reader.GetOrdinal("B2CMailServerPort")),
                    B2CMailUserName = reader.IsDBNull(reader.GetOrdinal("B2CMailUserName")) ? null : reader.GetString(reader.GetOrdinal("B2CMailUserName")),
                    B2CMailPassword = reader.IsDBNull(reader.GetOrdinal("B2CMailPassword")) ? null : reader.GetString(reader.GetOrdinal("B2CMailPassword")),
                    B2CUnvan = reader.IsDBNull(reader.GetOrdinal("B2CUnvan")) ? null : reader.GetString(reader.GetOrdinal("B2CUnvan")),
                    SegmentCariKodu = reader.IsDBNull(reader.GetOrdinal("SegmentCariKodu")) ? null : reader.GetString(reader.GetOrdinal("SegmentCariKodu")),
                    SegmentOndalikAyraci = reader.IsDBNull(reader.GetOrdinal("SegmentOndalikAyraci")) ? null : reader.GetString(reader.GetOrdinal("SegmentOndalikAyraci")),
                    ParamClientCode = reader.IsDBNull(reader.GetOrdinal("ParamClientCode")) ? null : reader.GetString(reader.GetOrdinal("ParamClientCode")),
                    ParamClientUserName = reader.IsDBNull(reader.GetOrdinal("ParamClientUserName")) ? null : reader.GetString(reader.GetOrdinal("ParamClientUserName")),
                    ParamClientPassword = reader.IsDBNull(reader.GetOrdinal("ParamClientPassword")) ? null : reader.GetString(reader.GetOrdinal("ParamClientPassword")),
                    ParamClientGUID = reader.IsDBNull(reader.GetOrdinal("ParamClientGUID")) ? null : reader.GetString(reader.GetOrdinal("ParamClientGUID")),
                    OzanApiKey = reader.IsDBNull(reader.GetOrdinal("OzanApiKey")) ? null : reader.GetString(reader.GetOrdinal("OzanApiKey")),
                    OzanSecretKey = reader.IsDBNull(reader.GetOrdinal("OzanSecretKey")) ? null : reader.GetString(reader.GetOrdinal("OzanSecretKey")),
                    PayTrMerchantId = reader.IsDBNull(reader.GetOrdinal("PayTrMerchantId")) ? null : reader.GetString(reader.GetOrdinal("PayTrMerchantId")),
                    PayTrMerchantKey = reader.IsDBNull(reader.GetOrdinal("PayTrMerchantKey")) ? null : reader.GetString(reader.GetOrdinal("PayTrMerchantKey")),
                    PayTrMerchantSalt = reader.IsDBNull(reader.GetOrdinal("PayTrMerchantSalt")) ? null : reader.GetString(reader.GetOrdinal("PayTrMerchantSalt"))
                };
            }
            return null;
        }

        public async Task<List<TaksitSecenekDto>> GetTaksitSecenekleriAsync(int uyeId, int posNo)
        {
            var list = new List<TaksitSecenekDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetTaksitSecenekleri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@PosNo", SqlDbType.Int) { Value = posNo });

            using var reader = await cmd.ExecuteReaderAsync();
            
            // Skip the first result set
            if (await reader.NextResultAsync())
            {
                // Read the second result set (TaksitSecenekDto)
                while (await reader.ReadAsync())
                {
                    list.Add(new TaksitSecenekDto
                    {
                        KrediKartiId = !reader.IsDBNull(reader.GetOrdinal("KrediKartiId")) ? reader.GetInt32(reader.GetOrdinal("KrediKartiId")) : 0,
                        KrediKarti = !reader.IsDBNull(reader.GetOrdinal("KrediKarti")) ? reader.GetString(reader.GetOrdinal("KrediKarti")) : null,
                        Logo = !reader.IsDBNull(reader.GetOrdinal("Logo")) ? reader.GetString(reader.GetOrdinal("Logo")) : null,
                        Taksit = !reader.IsDBNull(reader.GetOrdinal("Taksit")) ? reader.GetInt32(reader.GetOrdinal("Taksit")) : null,
                        Oran = !reader.IsDBNull(reader.GetOrdinal("Oran")) ? reader.GetDecimal(reader.GetOrdinal("Oran")) : null,
                        RowSpan = !reader.IsDBNull(reader.GetOrdinal("RowSpan")) ? reader.GetInt32(reader.GetOrdinal("RowSpan")) : null
                    });
                }
            }
            
            return list;
        }

        public async Task<List<OdemeSecenekDto>> GetOdemeSecenekleriAsync(int binKodu, int uyeId, int posNo)
        {
            var list = new List<OdemeSecenekDto>();
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetOdemeSecenekleri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@BinKodu", SqlDbType.Int) { Value = binKodu });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@PosNo", SqlDbType.Int) { Value = posNo });

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new OdemeSecenekDto
                {
                    KrediKartiId = !reader.IsDBNull(reader.GetOrdinal("KrediKartiId")) ? reader.GetString(reader.GetOrdinal("KrediKartiId")) : null,
                    NKodKrediKarti = !reader.IsDBNull(reader.GetOrdinal("nKodKrediKarti")) ? reader.GetInt32(reader.GetOrdinal("nKodKrediKarti")) : null,
                    KrediKarti = !reader.IsDBNull(reader.GetOrdinal("KrediKarti")) ? reader.GetString(reader.GetOrdinal("KrediKarti")) : null,
                    Logo = !reader.IsDBNull(reader.GetOrdinal("Logo")) ? reader.GetString(reader.GetOrdinal("Logo")) : null,
                    Taksit = !reader.IsDBNull(reader.GetOrdinal("Taksit")) ? reader.GetInt32(reader.GetOrdinal("Taksit")) : null,
                    Oran = !reader.IsDBNull(reader.GetOrdinal("Oran")) ? reader.GetDecimal(reader.GetOrdinal("Oran")) : null
                });
            }
            return list;
        }

        public async Task<DekontDto?> GetDekontBilgileriAsync(int odemeId, int uyeId, string odemeKey)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_GetDekontBilgileri";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@OdemeId", SqlDbType.Int) { Value = odemeId });
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@OdemeKey", SqlDbType.VarChar, 100) { Value = odemeKey ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new DekontDto
                {
                    Musteri = !reader.IsDBNull(reader.GetOrdinal("Musteri")) ? reader.GetString(reader.GetOrdinal("Musteri")) : null,
                    OdemeId = reader.GetInt32(reader.GetOrdinal("OdemeId")),
                    OdenenTutar = !reader.IsDBNull(reader.GetOrdinal("OdenenTutar")) ? reader.GetDecimal(reader.GetOrdinal("OdenenTutar")) : null,
                    ParaBirimi = !reader.IsDBNull(reader.GetOrdinal("ParaBirimi")) ? reader.GetString(reader.GetOrdinal("ParaBirimi")) : null,
                    KrediKarti = !reader.IsDBNull(reader.GetOrdinal("KrediKarti")) ? reader.GetString(reader.GetOrdinal("KrediKarti")) : null,
                    Taksit = !reader.IsDBNull(reader.GetOrdinal("Taksit")) ? reader.GetInt32(reader.GetOrdinal("Taksit")) : null,
                    OdemeTarihi = !reader.IsDBNull(reader.GetOrdinal("OdemeTarihi")) ? reader.GetDateTime(reader.GetOrdinal("OdemeTarihi")) : null,
                    Kartno = !reader.IsDBNull(reader.GetOrdinal("Kartno")) ? reader.GetString(reader.GetOrdinal("Kartno")) : null,
                    KartSahibi = !reader.IsDBNull(reader.GetOrdinal("KartSahibi")) ? reader.GetString(reader.GetOrdinal("KartSahibi")) : null,
                    IPAdres = !reader.IsDBNull(reader.GetOrdinal("IPAdres")) ? reader.GetString(reader.GetOrdinal("IPAdres")) : null
                };
            }
            return null;
        }

        public async Task<OdemeUpdateDto?> UpdateOdemeAsync(
            int odemeId,
            string odemeKey,
            string cMesaj,
            string bSonucDurum,
            string oSonuc)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdOdeme";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@OdemeId", SqlDbType.Int) { Value = odemeId });
            cmd.Parameters.Add(new SqlParameter("@OdemeKey", SqlDbType.VarChar, 100) { Value = odemeKey ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@cMesaj", SqlDbType.VarChar, -1) { Value = cMesaj ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@bSonucDurum", SqlDbType.VarChar, -1) { Value = bSonucDurum ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@oSonuc", SqlDbType.VarChar, -1) { Value = oSonuc ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OdemeUpdateDto
                {
                    Sonuc = reader.GetInt32(reader.GetOrdinal("Sonuc")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    Eposta = !reader.IsDBNull(reader.GetOrdinal("Eposta")) ? reader.GetString(reader.GetOrdinal("Eposta")) : null,
                    Parola = !reader.IsDBNull(reader.GetOrdinal("Parola")) ? reader.GetString(reader.GetOrdinal("Parola")) : null
                };
            }
            return null;
        }

        public async Task InsertParamLogAsync(
            int uyeId,
            long islemId,
            string sonuc,
            string sonucStr,
            int bankaSonucKod,
            string ucdUrl,
            string siparisId,
            string hostIpAdres,
            string serverIPAdres)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_InsParamLog";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@UyeId", SqlDbType.Int) { Value = uyeId });
            cmd.Parameters.Add(new SqlParameter("@IslemId", SqlDbType.BigInt) { Value = islemId });
            cmd.Parameters.Add(new SqlParameter("@Sonuc", SqlDbType.VarChar, 50) { Value = sonuc ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SonucStr", SqlDbType.VarChar, 50) { Value = sonucStr ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@BankaSonucKod", SqlDbType.Int) { Value = bankaSonucKod });
            cmd.Parameters.Add(new SqlParameter("@Ucd_Url", SqlDbType.VarChar, 50) { Value = ucdUrl ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SiparisId", SqlDbType.VarChar, 50) { Value = siparisId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@HostIpAdres", SqlDbType.VarChar, 50) { Value = hostIpAdres ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@ServerIPAdres", SqlDbType.VarChar, 50) { Value = serverIPAdres ?? (object)DBNull.Value });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<OdemeUpdateDto?> UpdateOzanOdemeAsync(
            int odemeId,
            string ozanStatus,
            string ozanMessage,
            string ozanTransactionId,
            decimal ozanOdemeTutari,
            string ozanCheckSum,
            string ozanCustomData,
            bool odemeBasariliMi,
            string odemeKey)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdOzanOdeme";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@OdemeId", SqlDbType.Int) { Value = odemeId });
            cmd.Parameters.Add(new SqlParameter("@OzanStatus", SqlDbType.VarChar, 500) { Value = ozanStatus ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OzanMessage", SqlDbType.VarChar, 500) { Value = ozanMessage ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OzanTransactionId", SqlDbType.VarChar, 500) { Value = ozanTransactionId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OzanOdemeTutari", SqlDbType.Decimal) { Value = ozanOdemeTutari });
            cmd.Parameters.Add(new SqlParameter("@OzanCheckSum", SqlDbType.VarChar, 500) { Value = ozanCheckSum ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OzanCustomData", SqlDbType.VarChar, -1) { Value = ozanCustomData ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OdemeBasariliMi", SqlDbType.Bit) { Value = odemeBasariliMi });
            cmd.Parameters.Add(new SqlParameter("@OdemeKey", SqlDbType.VarChar, 100) { Value = odemeKey ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OdemeUpdateDto
                {
                    Sonuc = reader.GetInt32(reader.GetOrdinal("Sonuc")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    Eposta = !reader.IsDBNull(reader.GetOrdinal("Eposta")) ? reader.GetString(reader.GetOrdinal("Eposta")) : null,
                    Parola = !reader.IsDBNull(reader.GetOrdinal("Parola")) ? reader.GetString(reader.GetOrdinal("Parola")) : null
                };
            }
            return null;
        }

        public async Task<OdemeUpdateDto?> UpdateParamOdemeAsync(
            int odemeId,
            string paramSonuc,
            string paramDekontId,
            decimal paramTahsilatTutari,
            decimal paramOdemeTutari,
            string paramSiparisId,
            string paramExtData,
            bool odemeBasariliMi,
            string odemeKey)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdParamOdeme";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@OdemeId", SqlDbType.Int) { Value = odemeId });
            cmd.Parameters.Add(new SqlParameter("@ParamSonuc", SqlDbType.VarChar, 500) { Value = paramSonuc ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@ParamDekontId", SqlDbType.VarChar, 500) { Value = paramDekontId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@ParamTahsilatTutari", SqlDbType.Decimal) { Value = paramTahsilatTutari });
            cmd.Parameters.Add(new SqlParameter("@ParamOdemeTutari", SqlDbType.Decimal) { Value = paramOdemeTutari });
            cmd.Parameters.Add(new SqlParameter("@ParamSiparisId", SqlDbType.VarChar, 500) { Value = paramSiparisId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@ParamExtData", SqlDbType.VarChar, -1) { Value = paramExtData ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OdemeBasariliMi", SqlDbType.Bit) { Value = odemeBasariliMi });
            cmd.Parameters.Add(new SqlParameter("@OdemeKey", SqlDbType.VarChar, 100) { Value = odemeKey ?? (object)DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OdemeUpdateDto
                {
                    Sonuc = reader.GetInt32(reader.GetOrdinal("Sonuc")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    Eposta = !reader.IsDBNull(reader.GetOrdinal("Eposta")) ? reader.GetString(reader.GetOrdinal("Eposta")) : null,
                    Parola = !reader.IsDBNull(reader.GetOrdinal("Parola")) ? reader.GetString(reader.GetOrdinal("Parola")) : null
                };
            }
            return null;
        }

        public async Task<OdemeUpdateDto?> UpdatePayTrOdemeAsync(
            int odemeId,
            string payTrStatus,
            decimal payTrTotalAmount,
            string payTrHash,
            bool payTrTestMode,
            string payTrPaymentType,
            string payTrCurrency,
            int payTrPaymentAmount,
            string payTrFailedCode,
            string payTrFailedMsg,
            bool odemeBasariliMi)
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.B2B_UpdPayTrOdeme";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@OdemeId", SqlDbType.Int) { Value = odemeId });
            cmd.Parameters.Add(new SqlParameter("@PayTrStatus", SqlDbType.VarChar, 500) { Value = payTrStatus ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PayTrTotalAmount", SqlDbType.Decimal) { Value = payTrTotalAmount });
            cmd.Parameters.Add(new SqlParameter("@PayTrHash", SqlDbType.VarChar, -1) { Value = payTrHash ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PayTrTestMode", SqlDbType.Bit) { Value = payTrTestMode });
            cmd.Parameters.Add(new SqlParameter("@PayTrPaymentType", SqlDbType.VarChar, 500) { Value = payTrPaymentType ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PayTrCurrency", SqlDbType.VarChar, 500) { Value = payTrCurrency ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PayTrPaymentAmount", SqlDbType.Int) { Value = payTrPaymentAmount });
            cmd.Parameters.Add(new SqlParameter("@PayTrFailedCode", SqlDbType.VarChar, 500) { Value = payTrFailedCode ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PayTrFailedMsg", SqlDbType.VarChar, 500) { Value = payTrFailedMsg ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@OdemeBasariliMi", SqlDbType.Bit) { Value = odemeBasariliMi });

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OdemeUpdateDto
                {
                    Sonuc = reader.GetInt32(reader.GetOrdinal("Sonuc")),
                    UyeId = reader.GetInt32(reader.GetOrdinal("UyeId")),
                    Eposta = !reader.IsDBNull(reader.GetOrdinal("Eposta")) ? reader.GetString(reader.GetOrdinal("Eposta")) : null,
                    Parola = !reader.IsDBNull(reader.GetOrdinal("Parola")) ? reader.GetString(reader.GetOrdinal("Parola")) : null
                };
            }
            return null;
        }
    }
}
