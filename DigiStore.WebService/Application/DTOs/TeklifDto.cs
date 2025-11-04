using System;
using System.Collections.Generic;

namespace DigiStore.WebService.Application.DTOs
{
    public class TeklifDto
    {
        public int TeklifId { get; set; }
        public int? UyeId { get; set; }
        public string? TeklifAdi { get; set; }
        public decimal? Oran { get; set; }
        public string? Aciklama { get; set; }
        public string? ParaBirimi { get; set; }
        public decimal? DovizKuru { get; set; }
        public DateTime? GecerlilikTarihi { get; set; }
        public DateTime? TeklifTarihi { get; set; }

        public decimal? AraToplam { get; set; }
        public decimal? ToplamKDV { get; set; }
        public decimal? ToplamTutar { get; set; }

        public decimal? AraToplamTL { get; set; }
        public decimal? ToplamKDVTL { get; set; }
        public decimal? ToplamTutarTL { get; set; }

        // Strongly-typed details
        public List<TeklifDetayDto>? Details { get; set; }

        // For HTML variant
        public string? Html { get; set; }
    }
}