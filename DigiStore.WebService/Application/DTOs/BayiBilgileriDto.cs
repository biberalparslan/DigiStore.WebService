namespace DigiStore.WebService.Application.DTOs
{
    public class BayiBilgileriDto
    {
        public int UyeId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Name { get; set; }
        public string? Eposta { get; set; }
        public string? Parola { get; set; }
        public bool DogrulandiMi { get; set; }
        public int KullaniciId { get; set; }
        public string? AdSoyad { get; set; }
        public string? Telefon { get; set; }
        public string? ResimYolu { get; set; }
        public int? BegeniAdet { get; set; }
        public int? SepetAdet { get; set; }
        public decimal? ToplamTutar { get; set; }
        public string? ParaBirimi { get; set; }
        public string? SirketLogoPath { get; set; }
    }
}
