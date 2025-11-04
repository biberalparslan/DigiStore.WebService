namespace DigiStore.WebService.Application.DTOs
{
    public class OdemeInsertDto
    {
        public string? SonKullanmaAy { get; set; }
        public string? SonKullanmaYil { get; set; }
        public string? CVV { get; set; }
        public decimal OdenecekTutar { get; set; }
        public int BankaKodu { get; set; }
        public int NKodPosHesaplari { get; set; }
        public int EklenenTaksit { get; set; }
        public string? KartSahibi { get; set; }
        public string? KartNo { get; set; }
        public int Taksit { get; set; }
        public string? IPAdres { get; set; }
        public int OdemeId { get; set; }
        public string? CariTelefon { get; set; }
        public string? OdemeKey { get; set; }
        public string? Domain { get; set; }
        public string? MailServer { get; set; }
        public int? MailServerPort { get; set; }
        public string? MailUserName { get; set; }
        public string? MailPassword { get; set; }
        public string? Unvan { get; set; }
        public string? B2CDomain { get; set; }
        public string? B2CMailServer { get; set; }
        public int? B2CMailServerPort { get; set; }
        public string? B2CMailUserName { get; set; }
        public string? B2CMailPassword { get; set; }
        public string? B2CUnvan { get; set; }
        public string? SegmentCariKodu { get; set; }
        public string? SegmentOndalikAyraci { get; set; }
        public string? ParamClientCode { get; set; }
        public string? ParamClientUserName { get; set; }
        public string? ParamClientPassword { get; set; }
        public string? ParamClientGUID { get; set; }
        public string? OzanApiKey { get; set; }
        public string? OzanSecretKey { get; set; }
        public string? PayTrMerchantId { get; set; }
        public string? PayTrMerchantKey { get; set; }
        public string? PayTrMerchantSalt { get; set; }
    }
}
