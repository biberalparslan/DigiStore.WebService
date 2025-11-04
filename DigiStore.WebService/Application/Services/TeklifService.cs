using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class TeklifService : ITeklifService
    {
        private readonly TeklifRepository _repo;

        public TeklifService(TeklifRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TeklifDto>> GetTekliflerAsync(int uyeId)
        {
            return await _repo.GetTekliflerAsync(uyeId);
        }

        public async Task<TeklifDto?> GetTeklifDetayAsync(int teklifId, int uyeId)
        {
            return await _repo.GetTeklifDetayAsync(teklifId, uyeId);
        }

        public async Task<string?> GetTeklifDetayHtmlAsync(int teklifId, int uyeId)
        {
            return await _repo.GetTeklifDetayHtmlAsync(teklifId, uyeId);
        }

        public async Task<bool> UpdateTeklifAsync(TeklifDto dto)
        {
            return await _repo.UpdateTeklifAsync(dto);
        }

        public async Task<bool> UpdateTeklifFiyatAsync(int uyeId, int teklifId, int urunId, decimal birimFiyat)
        {
            return await _repo.UpdateTeklifFiyatAsync(uyeId, teklifId, urunId, birimFiyat);
        }

        public async Task<bool> DeleteTeklifAsync(int teklifId, int uyeId)
        {
            return await _repo.DeleteTeklifAsync(teklifId, uyeId);
        }

        public async Task<int> InsertSiparisFromTeklifAsync(int teklifId, int uyeId, int teslimatAdresId = -1)
        {
            return await _repo.InsertSiparisFromTeklifAsync(teklifId, uyeId, teslimatAdresId);
        }
    }
}
