namespace DigiStore.WebService.Application.DTOs
{
    public class UrunDetayDto
    {
        public int UrunId { get; set; }
        public int? AnaKategoriId { get; set; }
        public string? AnaKategoriAdi { get; set; }
        public int? KategoriId { get; set; }
        public string? KategoriAdi { get; set; }
        public string? UrunAdi { get; set; }
        public int? MarkaId { get; set; }
        public string? MarkaAdi { get; set; }
        public int? KdvOrani { get; set; }
        public string? UrunDetaylari { get; set; }
        public string? Tanimlama { get; set; }
        public string? AnahtarKelime { get; set; }
        public decimal? NFiyat { get; set; }
        public decimal? NEskiFiyat { get; set; }
        public string? ParaBirimi { get; set; }
        public int? StokMiktari { get; set; }
        public string? StokMiktariPlus { get; set; }
        public string? ClassStock { get; set; }
        public string? ClassStokYok { get; set; }
        public string? ClassStokSorunuz { get; set; }
        public long? RN { get; set; }
        public string? IndirimOrani { get; set; }
        public string? IndirimliMi { get; set; }
        public string? ClassIndirimOrani { get; set; }
        public string? Fiyat { get; set; }
        public string? EskiFiyat { get; set; }
    }
}
