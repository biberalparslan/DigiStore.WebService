using System;

namespace DigiStore.WebService.Application.DTOs
{
    public class SepetDto
    {
        public int UrunId { get; set; }
        public string? KucukResimAdi { get; set; }
        public string? UrunAdi { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? ToplamFiyat { get; set; }
        public decimal? AraToplam { get; set; }
        public int? KdvOrani { get; set; }
        public decimal? KDV { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? TLFiyat { get; set; }
        public int? Adet { get; set; }
        public int? StokMiktari { get; set; }
        public decimal? SepetTutar { get; set; }
        public int? SepetAdedi { get; set; }
        public decimal? SepetKDV { get; set; }
        public decimal? SepetAraToplam { get; set; }
    }
}