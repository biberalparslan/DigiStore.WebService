using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class UrunService : IUrunService
    {
        private readonly UrunRepository _repo;

        public UrunService(UrunRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UrunByMarkaDto>> GetUrunByMarkaAsync(int markaId, int uyeId)
        {
            return await _repo.GetUrunByMarkaAsync(markaId, uyeId);
        }

        public async Task<IEnumerable<UrunByKategoriDto>> GetUrunByKategoriAsync(int kategoriId, int uyeId)
        {
            return await _repo.GetUrunByKategoriAsync(kategoriId, uyeId);
        }

        public async Task<IEnumerable<UrunByAramaDto>> GetUrunByAramaAsync(string query, int uyeId)
        {
            return await _repo.GetUrunByAramaAsync(query, uyeId);
        }

        public async Task<IEnumerable<UrunByAnaKategoriDto>> GetUrunByAnaKategoriAsync(int anaKategoriId, int uyeId)
        {
            return await _repo.GetUrunByAnaKategoriAsync(anaKategoriId, uyeId);
        }

        public async Task<IEnumerable<TumUrunlerDto>> GetTumUrunlerAsync(int? uyeId)
        {
            return await _repo.GetTumUrunlerAsync(uyeId);
        }

        public async Task<IEnumerable<UrunCokSatilanDto>> GetCokSatilanlarAsync(int uyeId)
        {
            return await _repo.GetCokSatilanlarAsync(uyeId);
        }

        public async Task<IEnumerable<UrunFirsatDto>> GetFirsatUrunleriAsync(int uyeId)
        {
            return await _repo.GetFirsatUrunleriAsync(uyeId);
        }

        public async Task<IEnumerable<UrunIlgiliDto>> GetIlgiliUrunlerAsync(int uyeId, int markaId, int kategoriId, int urunId)
        {
            return await _repo.GetIlgiliUrunlerAsync(uyeId, markaId, kategoriId, urunId);
        }

        public async Task<IEnumerable<UrunIndirimliDto>> GetIndirimliUrunlerAsync(int uyeId)
        {
            return await _repo.GetIndirimliUrunlerAsync(uyeId);
        }

        public async Task<IEnumerable<UrunSonEklenenDto>> GetSonEklenenlerAsync(int uyeId)
        {
            return await _repo.GetSonEklenenlerAsync(uyeId);
        }

        public async Task<IEnumerable<UrunSonGezilenDto>> GetSonGezilenlerAsync(int uyeId)
        {
            return await _repo.GetSonGezilenlerAsync(uyeId);
        }

        public async Task<IEnumerable<UrunGarantiDto>> GetGarantiAsync(int uyeId, string barkod)
        {
            return await _repo.GetGarantiAsync(uyeId, barkod);
        }

        public async Task<IEnumerable<UrunSearchBarDto>> GetUrunBySearchBarAsync(string query, int uyeId)
        {
            return await _repo.GetUrunBySearchBarAsync(query, uyeId);
        }

        public Task<(UrunDetayDto? detay, List<UrunDetayResimDto> resimler, List<UrunOzellikDto> ozellikler)> GetUrunDetayAsync(int urunId, int uyeId)
            => _repo.GetUrunDetayAsync(urunId, uyeId);
    }
}
