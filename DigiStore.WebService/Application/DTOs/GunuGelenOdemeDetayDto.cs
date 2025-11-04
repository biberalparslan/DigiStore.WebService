namespace DigiStore.WebService.Application.DTOs
{
    public class GunuGelenOdemeDetayDto
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        public decimal? Tutar { get; set; }
        public int? CurrencyId { get; set; }
        public string? HareketTipi { get; set; }
        public string? ParaBirimi { get; set; }
        public string? PbSembol { get; set; }
        public int? UyeCurr { get; set; }
        public string? UyeParaBirimi { get; set; }
        public decimal? KalanTutar { get; set; }
        public decimal? KalanTutarTL { get; set; }
        public DateTime? VadeTarihi { get; set; }
        public int? VadeGecenGun { get; set; }
        public string? VadeGecmisMi { get; set; }
        public decimal? ToplamTutar { get; set; }
    }
}
