using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class SepetService : ISepetService
    {
        private readonly SepetRepository _repo;
        public SepetService(SepetRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SepetDto>> GetSepetimAsync(int uyeId)
        {
            return await _repo.GetSepetimAsync(uyeId);
        }

        public async Task<bool> InsertSepetAsync(int uyeId, int urunId, int adet)
        {
            return await _repo.InsertSepetAsync(uyeId, urunId, adet);
        }

        public async Task<bool> UpdateSepetAdetAsync(int uyeId, int urunId, int adet)
        {
            return await _repo.UpdateSepetAdetAsync(uyeId, urunId, adet);
        }

        public async Task<bool> DeleteSepetAsync(int uyeId, int urunId)
        {
            return await _repo.DeleteSepetAsync(uyeId, urunId);
        }

        public async Task<int> InsertSiparisAsync(int uyeId)
        {
            return await _repo.InsertSiparisAsync(uyeId);
        }
    }
}
