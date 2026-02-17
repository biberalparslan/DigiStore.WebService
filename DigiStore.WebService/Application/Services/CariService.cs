using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class CariService : ICariService
    {
        private readonly CariRepository _repo;
        public CariService(CariRepository repo) { _repo = repo; }

        public Task<CariBilgilerDto?> GetCariBilgilerimAsync(int uyeId) => _repo.GetCariBilgilerimAsync(uyeId);
        public Task<List<CariGenelDurumDto>> GetCariGenelDurumAsync(int uyeId, int yil, int currencyId) => _repo.GetCariGenelDurumAsync(uyeId, yil, currencyId);
        public Task<List<CariGenelDurumDetayDto>> GetCariGenelDurumDetayAsync(int uyeId, DateTime dateBas, DateTime dateSon, int currencyId) => _repo.GetCariGenelDurumDetayAsync(uyeId, dateBas, dateSon, currencyId);
        public Task<List<CariGenelDurumDetayDto>> GetCariGenelDurumDetayAllAsync(DateTime? dateBas = null, DateTime? dateSon = null, int? currencyId = null) => _repo.GetCariGenelDurumDetayAllAsync(dateBas, dateSon, currencyId);
        public Task<List<CariHareketDetayDto>> GetCariHareketByIdAsync(int hareketId, int uyeId) => _repo.GetCariHareketByIdAsync(hareketId, uyeId);
        public Task<List<CariHareketDto>> GetCariHareketlerAsync(int uyeId, int tarih) => _repo.GetCariHareketlerAsync(uyeId, tarih);
        public Task<List<CariParaBirimiDto>> GetCariParaBirimleriAsync(int uyeId) => _repo.GetCariParaBirimleriAsync(uyeId);
        public Task<(List<GunuGelenOdemeDetayDto> detaylar, List<GunuGelenOdemeDto> ozet)> GetGunuGelenOdemelerAsync(int uyeId) => _repo.GetGunuGelenOdemelerAsync(uyeId);
        public Task<List<SiparisDto>> GetSiparislerimAsync(int uyeId, DateTime dateBas, DateTime dateSon) => _repo.GetSiparislerimAsync(uyeId, dateBas, dateSon);
        public Task<(SiparisDetayDto? siparis, List<SiparisDetayUrunDto> urunler, List<SiparisDetayTutarDto> tutarlar)> GetSiparisDetayAsync(int satisId, int uyeId) => _repo.GetSiparisDetayAsync(satisId, uyeId);
        public Task<BayiBilgileriDto?> GetBayiDogrulamaAsync(string bayiKodu, string eposta, string parola) => _repo.GetBayiDogrulamaAsync(bayiKodu, eposta, parola);
        public Task<CariBilgilerTutarDto?> GetUyeBilgileriAsync(int uyeId) => _repo.GetUyeBilgileriAsync(uyeId);
        public Task InsertReCaptchaSkorAsync(decimal rcSkor, string bayiKodu, string eposta, string sifre) => _repo.InsertReCaptchaSkorAsync(rcSkor, bayiKodu, eposta, sifre);
        public Task<int> InsertUyeBasvuruAsync(byte[] dosya, string dosyaAdi, string firmaAdi, string firmaYetkili, string email, string sifre, string telefon, string adres, string vergiDairesi, string vergiNo, int sehirId, int ilceId)
            => _repo.InsertUyeBasvuruAsync(dosya, dosyaAdi, firmaAdi, firmaYetkili, email, sifre, telefon, adres, vergiDairesi, vergiNo, sehirId, ilceId);
        public Task<int> UpdateUyeSifreAsync(int uyeId, string vKod, string sifre) => _repo.UpdateUyeSifreAsync(uyeId, vKod, sifre);
        public Task<ParolaYenilemeKoduDto?> GetParolaYenilemeKoduAsync(string eposta) => _repo.GetParolaYenilemeKoduAsync(eposta);
        public Task UpdateSirketLogoAsync(int uyeId, string path) => _repo.UpdateSirketLogoAsync(uyeId, path);
        public Task<List<CariOdemeDto>> GetOdemelerimAsync(int uyeId, DateTime dateBas, DateTime dateSon) => _repo.GetOdemelerimAsync(uyeId, dateBas, dateSon);
        public Task<CariOdemeDetayDto?> GetOdemeDetayAsync(int paraHareketiId, int uyeId) => _repo.GetOdemeDetayAsync(paraHareketiId, uyeId);
        public Task<List<TumUyelerDto>> GetTumUyelerAsync() => _repo.GetTumUyelerAsync();
    }
}