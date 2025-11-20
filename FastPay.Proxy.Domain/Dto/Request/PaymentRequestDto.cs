namespace FastPay.Proxy.Domain.Dto.Request
{
	public class PaymentRequestDto
	{
		public decimal Amount { get; set; }
		public string Currency { get; set; }
	}
}
