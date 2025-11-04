using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;

namespace DigiStore.WebService.Application.Services
{
    public class AnaKategoriService : IAnaKategoriService
    {
        private readonly AnaKategoriRepository _repo;

        public AnaKategoriService(AnaKategoriRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AnaKategoriDto>> GetAnaKategoriAsync()
        {
            return await _repo.GetAnaKategoriAsync();
        }
    }
}
