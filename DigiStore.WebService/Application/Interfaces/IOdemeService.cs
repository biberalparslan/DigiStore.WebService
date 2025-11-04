using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IOdemeService
    {
        Task<OdemeInsertDto?> InsertOdemeAsync(int uyeId, decimal odenecekTutar, int krediKarti, int taksit, string kartSahibi, string kartNo, string ip);
        Task<List<TaksitSecenekDto>> GetTaksitSecenekleriAsync(int uyeId, int posNo);
        Task<List<OdemeSecenekDto>> GetOdemeSecenekleriAsync(int binKodu, int uyeId, int posNo);
        Task<DekontDto?> GetDekontBilgileriAsync(int odemeId, int uyeId, string odemeKey);
        Task<OdemeUpdateDto?> UpdateOdemeAsync(int odemeId, string odemeKey, string cMesaj, string bSonucDurum, string oSonuc);
        Task InsertParamLogAsync(int uyeId, long islemId, string sonuc, string sonucStr, int bankaSonucKod, string ucdUrl, string siparisId, string hostIpAdres, string serverIPAdres);
        Task<OdemeUpdateDto?> UpdateOzanOdemeAsync(int odemeId, string ozanStatus, string ozanMessage, string ozanTransactionId, decimal ozanOdemeTutari, string ozanCheckSum, string ozanCustomData, bool odemeBasariliMi, string odemeKey);
        Task<OdemeUpdateDto?> UpdateParamOdemeAsync(int odemeId, string paramSonuc, string paramDekontId, decimal paramTahsilatTutari, decimal paramOdemeTutari, string paramSiparisId, string paramExtData, bool odemeBasariliMi, string odemeKey);
        Task<OdemeUpdateDto?> UpdatePayTrOdemeAsync(int odemeId, string payTrStatus, decimal payTrTotalAmount, string payTrHash, bool payTrTestMode, string payTrPaymentType, string payTrCurrency, int payTrPaymentAmount, string payTrFailedCode, string payTrFailedMsg, bool odemeBasariliMi);
    }
}
