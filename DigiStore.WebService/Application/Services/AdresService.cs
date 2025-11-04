using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Repositories;
using AutoMapper;

namespace DigiStore.WebService.Application.Services
{
    public class AdresService : IAdresService
    {
        private readonly AddressRepository _repo;
        private readonly IMapper _mapper;

        public AdresService(AddressRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdresDto>> GetAdreslerAsync(int uyeId)
        {
            var list = await _repo.GetAdreslerAsync(uyeId);
            return list;
        }

        public async Task<AdresDto?> GetAdresByIdAsync(int adresId, int uyeId)
        {
            return await _repo.GetAdresByIdAsync(adresId, uyeId);
        }

        public async Task<bool> DelAdresAsync(int adresId, int uyeId)
        {
            return await _repo.DelAdresAsync(adresId, uyeId);
        }

        public async Task<bool> UpdDefAdresAsync(int adresId, int uyeId)
        {
            return await _repo.UpdDefAdresAsync(adresId, uyeId);
        }

        public async Task<int> UpdAdresAsync(AdresDto dto)
        {
            return await _repo.UpdAdresAsync(dto);
        }
    }
}
