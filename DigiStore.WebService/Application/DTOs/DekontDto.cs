namespace DigiStore.WebService.Application.DTOs
{
    public class DekontDto
    {
        public string? Musteri { get; set; }
        public int OdemeId { get; set; }
        public decimal? OdenenTutar { get; set; }
        public string? ParaBirimi { get; set; }
        public string? KrediKarti { get; set; }
        public int? Taksit { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        public string? Kartno { get; set; }
        public string? KartSahibi { get; set; }
        public string? IPAdres { get; set; }
    }
}
