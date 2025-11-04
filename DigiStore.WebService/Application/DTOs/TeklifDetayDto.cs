namespace DigiStore.WebService.Application.DTOs
{
    public class TeklifDetayDto
    {
        public int TeklifLinesId { get; set; }
        public int TeklifId { get; set; }
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public string? KucukResimAdi { get; set; }
        public decimal? GuncelFiyat { get; set; }
        public decimal? TeklifFiyat { get; set; }
        public int? Miktar { get; set; }
        public string? Birim { get; set; }
        public int? BirimId { get; set; }
        public decimal? KDVOrani { get; set; }
        public decimal? AlisFiyati { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? BirimFiyat { get; set; }
    }
}