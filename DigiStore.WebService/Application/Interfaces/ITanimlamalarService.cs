using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface ITanimlamalarService
    {
        Task<FirmaBilgileriDto?> GetFirmaBilgileriAsync();
        Task<List<SehirlerDto>> GetSehirlerAsync();
        Task<List<IlcelerDto>> GetIlcelerAsync(int sehirId);
        Task<List<DovizKuruDto>> GetKurBilgileriAsync();
        Task<List<MarkalarDto>> GetMarkalarAsync();
        Task<List<BannerResimDto>> GetBannerResimAsync();
        Task<List<VitrinResimDto>> GetVitrinResimAsync();
    }
}