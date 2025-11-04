using System;

namespace DigiStore.WebService.Application.DTOs
{
    public class UrunFirsatDto
    {
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public int? KdvOrani { get; set; }
        public string? OrtancaResimAdi { get; set; }
        public string? OrtancaResimAdi2 { get; set; }
        public decimal? NFiyat { get; set; }
        public decimal? NEskiFiyat { get; set; }
        public string? ParaBirimi { get; set; }
        public int? CurrencyId { get; set; }
        public int? StokMiktari { get; set; }
        public decimal? ToptanFiyat { get; set; }
        public decimal? IndirimliFiyat { get; set; }
        public decimal? Fiyat { get; set; }
        public string? IndirimOrani { get; set; }
        public string? ClassIndirimOrani { get; set; }
        public string? FiyatFormatted { get; set; }
        public string? EskiFiyat { get; set; }
        public string? FiyatTL { get; set; }
    }
}