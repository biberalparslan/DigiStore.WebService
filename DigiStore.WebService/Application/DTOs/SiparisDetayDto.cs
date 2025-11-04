namespace DigiStore.WebService.Application.DTOs
{
    public class SiparisDetayDto
    {
        public int SatisId { get; set; }

        public DateTime? SiparisTarihi { get; set; }
        public int? SiparisDurumId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? DovizKuru { get; set; }
        public string? SiparisDurum { get; set; }
        public string? ParaBirimi { get; set; }
        public int? TeslimatAdresId { get; set; }
        public int? UyeId { get; set; }
        public string? Kargo { get; set; }
        public string? OdemeTipi { get; set; }
        public string? KargoTipi { get; set; }
        public string? KargoTakipNo { get; set; }
        public string? KargoUrl { get; set; }
        public decimal? Tutar { get; set; }
        public string? GonderildiMi { get; set; }

        public string? TAdSoyad { get; set; }
        public string? TUnvan { get; set; }
        public string? TVergiDairesi { get; set; }
        public string? TVergiNo { get; set; }
        public string? TSehirAdi { get; set; }
        public string? TIlceAdi { get; set; }
        public string? TSahisAdres { get; set; }
        public string? TTelefon { get; set; }

        public string? AdSoyad { get; set; }
        public string? Unvan { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? SehirAdi { get; set; }
        public string? IlceAdi { get; set; }
        public string? SahisAdres { get; set; }
        public string? Telefon { get; set; }
    }
}
