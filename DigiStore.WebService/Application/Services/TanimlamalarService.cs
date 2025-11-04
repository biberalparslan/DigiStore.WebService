using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class TanimlamalarService : ITanimlamalarService
    {
        private readonly TanimlamalarRepository _repo;
        public TanimlamalarService(TanimlamalarRepository repo)
        {
            _repo = repo;
        }

        public Task<FirmaBilgileriDto?> GetFirmaBilgileriAsync() => _repo.GetFirmaBilgileriAsync();
        public Task<List<SehirlerDto>> GetSehirlerAsync() => _repo.GetSehirlerAsync();
        public Task<List<IlcelerDto>> GetIlcelerAsync(int sehirId) => _repo.GetIlcelerAsync(sehirId);
        public Task<List<DovizKuruDto>> GetKurBilgileriAsync() => _repo.GetKurBilgileriAsync();
        public Task<List<MarkalarDto>> GetMarkalarAsync() => _repo.GetMarkalarAsync();
        public Task<List<BannerResimDto>> GetBannerResimAsync() => _repo.GetBannerResimAsync();
        public Task<List<VitrinResimDto>> GetVitrinResimAsync() => _repo.GetVitrinResimAsync();
    }
}
