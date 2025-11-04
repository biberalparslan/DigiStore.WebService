namespace DigiStore.WebService.Application.DTOs
{
    public class CariHareketDetayDto
    {
        public int SatisId { get; set; }
        public int SatisLinesId { get; set; }
        public DateTime? SiparisTarihi { get; set; }
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? SatirTutar { get; set; }
        public string? Barkodlar { get; set; }
        public decimal? Iskonto { get; set; }
        public decimal? DovizKuru { get; set; }
        public decimal? ToplamTutar { get; set; }
        public decimal? ToplamTutarTL { get; set; }
        public string? ClassIsTL { get; set; }
    }
}
