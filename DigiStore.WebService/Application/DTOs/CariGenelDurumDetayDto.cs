namespace DigiStore.WebService.Application.DTOs
{
    public class CariGenelDurumDetayDto
    {
        public int UyeId { get; set; }
        public DateTime? Tarih { get; set; }
        public int? SeriNo { get; set; }
        public string? HareketTipi { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
    }
}
