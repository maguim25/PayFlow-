namespace FastPay.Proxy.Domain.Dto.Response
{
	public class SecurePayResponseDto
	{
		public string Transaction_Id { get; set; }
		public string NameProvider { get; set; }
		public decimal Fee { get; set; }
		public string Result { get; set; }
	}
}
