using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface ICariService
    {
        Task<CariBilgilerDto?> GetCariBilgilerimAsync(int uyeId);
        Task<List<CariGenelDurumDto>> GetCariGenelDurumAsync(int uyeId, int yil, int currencyId);
        Task<List<CariGenelDurumDetayDto>> GetCariGenelDurumDetayAsync(int uyeId, DateTime dateBas, DateTime dateSon, int currencyId);
        Task<List<CariHareketDetayDto>> GetCariHareketByIdAsync(int hareketId, int uyeId);
        Task<List<CariHareketDto>> GetCariHareketlerAsync(int uyeId, int tarih);
        Task<List<CariParaBirimiDto>> GetCariParaBirimleriAsync(int uyeId);
        Task<(List<GunuGelenOdemeDetayDto> detaylar, List<GunuGelenOdemeDto> ozet)> GetGunuGelenOdemelerAsync(int uyeId);
        Task<List<SiparisDto>> GetSiparislerimAsync(int uyeId, DateTime dateBas, DateTime dateSon);
        Task<(SiparisDetayDto? siparis, List<SiparisDetayUrunDto> urunler, List<SiparisDetayTutarDto> tutarlar)> GetSiparisDetayAsync(int satisId, int uyeId);
        Task<BayiBilgileriDto?> GetBayiDogrulamaAsync(string bayiKodu, string eposta, string parola);
        Task<CariBilgilerTutarDto?> GetUyeBilgileriAsync(int uyeId);
        Task InsertReCaptchaSkorAsync(decimal rcSkor, string bayiKodu, string eposta, string sifre);
        Task<int> InsertUyeBasvuruAsync(byte[] dosya, string dosyaAdi, string firmaAdi, string firmaYetkili, string email, string sifre, string telefon, string adres, string vergiDairesi, string vergiNo, int sehirId, int ilceId);
        Task<int> UpdateUyeSifreAsync(int uyeId, string vKod, string sifre);
        Task<ParolaYenilemeKoduDto?> GetParolaYenilemeKoduAsync(string eposta);
        Task UpdateSirketLogoAsync(int uyeId, string path);
        Task<List<CariOdemeDto>> GetOdemelerimAsync(int uyeId, DateTime dateBas, DateTime dateSon);
        Task<CariOdemeDetayDto?> GetOdemeDetayAsync(int paraHareketiId, int uyeId);
        Task<List<TumUyelerDto>> GetTumUyelerAsync();
    }
}