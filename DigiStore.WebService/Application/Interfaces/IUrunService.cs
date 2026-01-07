using System.Collections.Generic;
using System.Threading.Tasks;
using DigiStore.WebService.Application.DTOs;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IUrunService
    {
        Task<IEnumerable<UrunByMarkaDto>> GetUrunByMarkaAsync(int markaId, int uyeId);
        Task<IEnumerable<UrunByKategoriDto>> GetUrunByKategoriAsync(int kategoriId, int uyeId);
        Task<IEnumerable<UrunByAramaDto>> GetUrunByAramaAsync(string query, int uyeId);
        Task<IEnumerable<UrunByAnaKategoriDto>> GetUrunByAnaKategoriAsync(int anaKategoriId, int uyeId);
        Task<IEnumerable<TumUrunlerDto>> GetTumUrunlerAsync(int? uyeId);

        Task<IEnumerable<UrunCokSatilanDto>> GetCokSatilanlarAsync(int uyeId);
        Task<IEnumerable<UrunFirsatDto>> GetFirsatUrunleriAsync(int uyeId);
        Task<IEnumerable<UrunIlgiliDto>> GetIlgiliUrunlerAsync(int uyeId, int markaId, int kategoriId, int urunId);
        Task<IEnumerable<UrunIndirimliDto>> GetIndirimliUrunlerAsync(int uyeId);
        Task<IEnumerable<UrunSonEklenenDto>> GetSonEklenenlerAsync(int uyeId);
        Task<IEnumerable<UrunSonGezilenDto>> GetSonGezilenlerAsync(int uyeId);

        Task<IEnumerable<UrunGarantiDto>> GetGarantiAsync(int uyeId, string barkod);
        Task<IEnumerable<UrunSearchBarDto>> GetUrunBySearchBarAsync(string query, int uyeId);
        Task<(UrunDetayDto? detay, List<UrunDetayResimDto> resimler, List<UrunOzellikDto> ozellikler)> GetUrunDetayAsync(int urunId, int uyeId);
    }
}