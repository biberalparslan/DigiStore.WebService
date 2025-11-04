using System;

namespace DigiStore.WebService.Application.DTOs
{
    public class AdresDto
    {
        public int AdresId { get; set; }
        public int? UyeId { get; set; }
        public string? AdresAdi { get; set; }
        public string? AdSoyad { get; set; }
        public string? SahisAdres { get; set; }
        public string? SahisPostaKodu { get; set; }
        public string? EPosta { get; set; }
        public string? Firma { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? FirmaAdres { get; set; }
        public string? FirmaPostaKodu { get; set; }
        public string? Semt { get; set; }
        public int? IlceId { get; set; }
        public int? SehirId { get; set; }
        public int? UlkeId { get; set; }
        public string? Telefon { get; set; }
        public string? Telefon2 { get; set; }
        public string? AdresDetayi { get; set; }
        public bool? TeslimatAdresiMi { get; set; }
        public bool? FaturaAdresiMi { get; set; }
        public string? AnonimUyeId { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? SehirAdi { get; set; }
        public string? IlceAdi { get; set; }
        public bool DefaultMu { get; set; }
    }
}