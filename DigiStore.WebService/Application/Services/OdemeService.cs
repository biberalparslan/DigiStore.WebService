using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class OdemeService : IOdemeService
    {
        private readonly OdemeRepository _repo;

        public OdemeService(OdemeRepository repo)
        {
            _repo = repo;
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
            return await _repo.InsertOdemeAsync(uyeId, odenecekTutar, krediKarti, taksit, kartSahibi, kartNo, ip);
        }

        public async Task<List<TaksitSecenekDto>> GetTaksitSecenekleriAsync(int uyeId, int posNo)
        {
            return await _repo.GetTaksitSecenekleriAsync(uyeId, posNo);
        }

        public async Task<List<OdemeSecenekDto>> GetOdemeSecenekleriAsync(int binKodu, int uyeId, int posNo)
        {
            return await _repo.GetOdemeSecenekleriAsync(binKodu, uyeId, posNo);
        }

        public async Task<DekontDto?> GetDekontBilgileriAsync(int odemeId, int uyeId, string odemeKey)
        {
            return await _repo.GetDekontBilgileriAsync(odemeId, uyeId, odemeKey);
        }

        public async Task<OdemeUpdateDto?> UpdateOdemeAsync(int odemeId, string odemeKey, string cMesaj, string bSonucDurum, string oSonuc)
        {
            return await _repo.UpdateOdemeAsync(odemeId, odemeKey, cMesaj, bSonucDurum, oSonuc);
        }

        public async Task InsertParamLogAsync(int uyeId, long islemId, string sonuc, string sonucStr, int bankaSonucKod, string ucdUrl, string siparisId, string hostIpAdres, string serverIPAdres)
        {
            await _repo.InsertParamLogAsync(uyeId, islemId, sonuc, sonucStr, bankaSonucKod, ucdUrl, siparisId, hostIpAdres, serverIPAdres);
        }

        public async Task<OdemeUpdateDto?> UpdateOzanOdemeAsync(int odemeId, string ozanStatus, string ozanMessage, string ozanTransactionId, decimal ozanOdemeTutari, string ozanCheckSum, string ozanCustomData, bool odemeBasariliMi, string odemeKey)
        {
            return await _repo.UpdateOzanOdemeAsync(odemeId, ozanStatus, ozanMessage, ozanTransactionId, ozanOdemeTutari, ozanCheckSum, ozanCustomData, odemeBasariliMi, odemeKey);
        }

        public async Task<OdemeUpdateDto?> UpdateParamOdemeAsync(int odemeId, string paramSonuc, string paramDekontId, decimal paramTahsilatTutari, decimal paramOdemeTutari, string paramSiparisId, string paramExtData, bool odemeBasariliMi, string odemeKey)
        {
            return await _repo.UpdateParamOdemeAsync(odemeId, paramSonuc, paramDekontId, paramTahsilatTutari, paramOdemeTutari, paramSiparisId, paramExtData, odemeBasariliMi, odemeKey);
        }

        public async Task<OdemeUpdateDto?> UpdatePayTrOdemeAsync(int odemeId, string payTrStatus, decimal payTrTotalAmount, string payTrHash, bool payTrTestMode, string payTrPaymentType, string payTrCurrency, int payTrPaymentAmount, string payTrFailedCode, string payTrFailedMsg, bool odemeBasariliMi)
        {
            return await _repo.UpdatePayTrOdemeAsync(odemeId, payTrStatus, payTrTotalAmount, payTrHash, payTrTestMode, payTrPaymentType, payTrCurrency, payTrPaymentAmount, payTrFailedCode, payTrFailedMsg, odemeBasariliMi);
        }
    }
}
