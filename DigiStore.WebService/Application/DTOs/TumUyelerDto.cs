using System;

namespace DigiStore.WebService.Application.DTOs
{
    public class TumUyelerDto
    {
        public int UyeId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Unvan { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? Adres { get; set;  }
        public string? Eposta { get; set; }
        public string? Parola { get; set; }
        public bool? DogrulandiMi { get; set; }
        public bool? TumHaberler { get; set; }
        public DateTime? KayitTarihi { get; set; }
        public bool? ToptanMusterimi { get; set; }
        public bool? SilindiMi { get; set; }
        public bool? PasifMi { get; set; }
        public string? Telefon { get; set; }
        public string? Name { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Aciklama { get; set; }
        public bool? eFatura { get; set; }
        public string? CepTelefon { get; set; }
        public string? Telefon2 { get; set; }
        public decimal? Limit { get; set; }
        public string? SirketLogoPath { get; set; }
    }
}
