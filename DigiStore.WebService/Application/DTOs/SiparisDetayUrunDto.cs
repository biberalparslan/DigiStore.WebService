namespace DigiStore.WebService.Application.DTOs
{
    public class SiparisDetayUrunDto
    {
        public int SatisId { get; set; }

        public int? UrunId { get; set; }
        public string? UrunKodu { get; set; }
        public string? UrunAdi { get; set; }
        public int? KdvOrani { get; set; }
        public int? BirimId { get; set; }
        public string? BirimAdi { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? Toplam { get; set; }
    }
}
