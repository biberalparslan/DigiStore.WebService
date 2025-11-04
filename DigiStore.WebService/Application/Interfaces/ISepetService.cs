using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface ISepetService
    {
        Task<IEnumerable<SepetDto>> GetSepetimAsync(int uyeId);
        Task<bool> InsertSepetAsync(int uyeId, int urunId, int adet);
        Task<bool> UpdateSepetAdetAsync(int uyeId, int urunId, int adet);
        Task<bool> DeleteSepetAsync(int uyeId, int urunId);
        Task<int> InsertSiparisAsync(int uyeId);
    }
}