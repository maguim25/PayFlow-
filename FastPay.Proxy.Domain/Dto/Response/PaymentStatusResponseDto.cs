namespace FastPay.Proxy.Domain.Dto.Response
{
	public class PaymentStatusResponseDto
	{
		public int Id { get; set; }
		public string ExternalId { get; set; }
		public string Status { get; set; }
		public string Provider { get; set; }
		public decimal GrossAmount { get; set; }
		public decimal Fee { get; set; }
		public decimal NetAmount { get; set; }
	}
}
