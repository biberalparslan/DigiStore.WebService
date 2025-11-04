namespace DigiStore.WebService.Application.DTOs
{
    public class UrunlerDto
    {
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public string? KucukResimAdi { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? IndirimliFiyat { get; set; }
        public decimal? ToptanFiyat { get; set; }
        public int? MarkaId { get; set; }
        public int? KategoriId { get; set; }
        public int? AnaKategoriId { get; set; }
        public int? BirimId { get; set; }
        public string? Birim { get; set; }
        public bool? B2BUrunu { get; set; }
        public bool? B2CUrunu { get; set; }
        public int? Stok { get; set; }
        public int? CurrencyId { get; set; }
        public string? ParaBirimi { get; set; }
    }
}