namespace DigiStore.WebService.Application.DTOs
{
    public class CariHareketDto
    {
        public int UyeId { get; set; }
        public DateTime? Tarih { get; set; }
        public int Id { get; set; }
        public int? Tip { get; set; }
        public string? FaturaNo { get; set; }
        public string? ParaBirimi { get; set; }
        public string? URL { get; set; }
        public string? SeriNo { get; set; }
        public string? HareketTipi { get; set; }
        public string? ClassPdfIcon { get; set; }
        public string? ClassModal { get; set; }
        public decimal? Tutar { get; set; }
    }
}
