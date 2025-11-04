namespace DigiStore.WebService.Application.DTOs
{
    public class UrunCokSatilanDto
    {
        public long RN { get; set; }
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public int? KdvOrani { get; set; }
        public string? OrtancaResimAdi { get; set; }
        public string? OrtancaResimAdi2 { get; set; }
        public decimal? NFiyat { get; set; }
        public decimal? NEskiFiyat { get; set; }
        public int? StokMiktari { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Fiyat { get; set; }
        public string? EskiFiyat { get; set; }
    }
}
