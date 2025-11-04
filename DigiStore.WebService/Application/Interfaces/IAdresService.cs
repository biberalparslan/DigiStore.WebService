using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IAdresService
    {
        Task<IEnumerable<AdresDto>> GetAdreslerAsync(int uyeId);
        Task<AdresDto?> GetAdresByIdAsync(int adresId, int uyeId);
        Task<bool> DelAdresAsync(int adresId, int uyeId);
        Task<bool> UpdDefAdresAsync(int adresId, int uyeId);
        Task<int> UpdAdresAsync(AdresDto dto);
        // other methods will be added for other stored procedures
    }
}