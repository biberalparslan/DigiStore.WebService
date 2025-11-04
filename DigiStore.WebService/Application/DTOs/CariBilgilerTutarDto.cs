namespace DigiStore.WebService.Application.DTOs
{
    public class CariBilgilerTutarDto
    {
        public int UyeId { get; set; }
        public string? Unvan { get; set; }
        public string? VergiNo { get; set; }
        public string? VergiDairesi { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public int? CurrencyId { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal? BakiyeTL { get; set; }
        public decimal? VadeGecenGun { get; set; }
        public decimal? VadesiGecenTutar { get; set; }
        public decimal? VadesiGecenTL { get; set; }
        public decimal? VadesiGecenTopTL { get; set; }
        public decimal? BakiyeTopTL { get; set; }
    }
}
