using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IKategoriService
    {
        Task<IEnumerable<KategoriDto>> GetKategoriAsync(string dil, int anaKategoriId);
        Task<IEnumerable<PopulerKategoriDto>> GetPopulerKategoriAsync();
        Task<List<Dictionary<string, object?>>> GetBreadcrumbAsync(int anaKategoriId, int kategoriId);
    }
}