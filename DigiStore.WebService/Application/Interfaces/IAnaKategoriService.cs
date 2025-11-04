using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IAnaKategoriService
    {
        Task<IEnumerable<AnaKategoriDto>> GetAnaKategoriAsync();
    }
}