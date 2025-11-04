namespace DigiStore.WebService.Application.DTOs
{
    public class SiparisDto
    {
        public int SatisId { get; set; }
        public DateTime? SiparisTarihi { get; set; }
        public string? OdemeTipi { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? Tutar { get; set; }
        public string? ToplamTutar { get; set; }
    }
}
