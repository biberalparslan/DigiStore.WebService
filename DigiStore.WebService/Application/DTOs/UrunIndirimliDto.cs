namespace DigiStore.WebService.Application.DTOs
{
    public class UrunIndirimliDto
    {
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public int? KdvOrani { get; set; }
        public decimal? NFiyat { get; set; }
        public decimal? NEskiFiyat { get; set; }
        public string? ParaBirimi { get; set; }
        public int? StokMiktari { get; set; }
        public string? StokMiktariPlus { get; set; }
        public string? ClassStock { get; set; }
        public string? ClassStokYok { get; set; }
        public string? ClassStokSorunuz { get; set; }
        public string? OrtancaResimAdi { get; set; }
        public string? OrtancaResimAdi2 { get; set; }
        public long RN { get; set; }
        public string? IndirimOrani { get; set; }
        public string? ClassIndirimOrani { get; set; }
        public string? Fiyat { get; set; }
        public string? EskiFiyat { get; set; }
    }
}
