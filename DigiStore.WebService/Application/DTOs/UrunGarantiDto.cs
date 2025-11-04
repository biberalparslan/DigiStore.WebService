namespace DigiStore.WebService.Application.DTOs
{
    public class UrunGarantiDto
    {
        public int? Tip { get; set; }
        public string? UrunAdi { get; set; }
        public DateTime? GarantiBaslangic { get; set; }
        public DateTime? GarantiBitis { get; set; }
        public string? GarantiSuresi { get; set; }
    }
}
