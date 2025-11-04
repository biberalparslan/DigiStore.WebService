namespace DigiStore.WebService.Application.DTOs
{
    public class KategoriDto
    {
        public long? RN { get; set; }
        public int KategoriId { get; set; }
        public string? KategoriAdi { get; set; }
        public int AnaKategoriId { get; set; }
        public string? Resim { get; set; }
        public int? KategoriAdet { get; set; }
        public int? UrunAdet { get; set; }
    }
}
