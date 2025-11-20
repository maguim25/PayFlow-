namespace FastPay.Proxy.Domain.Dto
{
	public class SecurePayDto
	{
		public decimal Amount_Cents { get; set; }
		public string Currency_Code { get; set; }
		public string Client_Reference { get; set; }
	}
}
