using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface ITeklifService
    {
        Task<IEnumerable<TeklifDto>> GetTekliflerAsync(int uyeId);
        Task<TeklifDto?> GetTeklifDetayAsync(int teklifId, int uyeId);
        Task<string?> GetTeklifDetayHtmlAsync(int teklifId, int uyeId);
        Task<bool> UpdateTeklifAsync(TeklifDto dto);
        Task<bool> UpdateTeklifFiyatAsync(int uyeId, int teklifId, int urunId, decimal birimFiyat);
        Task<bool> DeleteTeklifAsync(int teklifId, int uyeId);
        Task<int> InsertSiparisFromTeklifAsync(int teklifId, int uyeId, int teslimatAdresId = -1);
    }
}