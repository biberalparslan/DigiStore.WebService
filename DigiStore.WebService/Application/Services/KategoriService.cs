using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class KategoriService : IKategoriService
    {
        private readonly KategoriRepository _repo;

        public KategoriService(KategoriRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<KategoriDto>> GetKategoriAsync(string dil, int anaKategoriId)
        {
            return await _repo.GetKategoriAsync(dil, anaKategoriId);
        }

        public async Task<IEnumerable<PopulerKategoriDto>> GetPopulerKategoriAsync()
        {
            return await _repo.GetPopulerKategoriAsync();
        }

        public Task<List<Dictionary<string, object?>>> GetBreadcrumbAsync(int anaKategoriId, int kategoriId)
        {
            return _repo.GetBreadcrumbAsync(anaKategoriId, kategoriId);
        }
    }
}
