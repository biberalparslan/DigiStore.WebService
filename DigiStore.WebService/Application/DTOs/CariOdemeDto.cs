namespace DigiStore.WebService.Application.DTOs
{
    public class CariOdemeDto
    {
        public int UyeId { get; set; }
        public int? ParaHareketiId { get; set; }
        public DateTime? HareketTarihi { get; set; }
        public string? BankaAdi { get; set; }
        public string? KartSahibi { get; set; }
        public string? KartNo { get; set; }
        public string? MusteriTemsilcisi { get; set; }
        public string? OdemeTipi { get; set; }
        public string? ParaBirimi { get; set; }
        public int? Taksit { get; set; }
        public decimal? Tutar { get; set; }
        public decimal? TutarTL { get; set; }
    }
}
