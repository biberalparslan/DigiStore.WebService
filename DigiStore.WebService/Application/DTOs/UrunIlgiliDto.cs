namespace DigiStore.WebService.Application.DTOs
{
    public class UrunIlgiliDto
    {
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public int? KdvOrani { get; set; }
        public int? MarkaId { get; set; }
        public int? KategoriId { get; set; }
        public decimal? NFiyat { get; set; }
        public decimal? NEskiFiyat { get; set; }
        public string? ParaBirimi { get; set; }
        public int? StokMiktari { get; set; }
        public string? OrtancaResimAdi { get; set; }
        public string? OrtancaResimAdi2 { get; set; }
        public long RN { get; set; }
        public string? IndirimOrani { get; set; }
        public string? ClassIndirimOrani { get; set; }
        public string? Fiyat { get; set; }
        public string? EskiFiyat { get; set; }
    }
}
