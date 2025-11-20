using FastPay.Proxy.Application.Interface;
using FastPay.Proxy.Domain.Dto;
using FastPay.Proxy.Domain.Dto.Request;
using FastPay.Proxy.Domain.Dto.Response;
using FastPay.Proxy.Domain.Enum;
using Microsoft.Extensions.Configuration;

namespace FastPay.Proxy.Application.Application
{
	public class ProxyFlow(IConfiguration configuration, ISecurePayProvider securePayProvider): IProxyFlow
	{
		private readonly IConfiguration _configuration = configuration;
		private readonly ISecurePayProvider _securePayProvider = securePayProvider;

		public async Task<SecurePayResponseDto> FlowPayment(PaymentRequestDto request, CancellationToken ct = default)
		{
			try
			{
				SecurePayResponseDto result;

				var security = new SecurePayDto
				{
					Amount_Cents = request.Amount,
					Currency_Code = request.Currency,
					Client_Reference = "ORD-20251022"
				};

				if (request.Amount < 100m)
				{
					result = await _securePayProvider.ProcessAsync(
						security, 
						(int)ProviderEnum.Default
					);
				}
				else
				{
					result = await _securePayProvider.ProcessAsync(
						security,
						(int)ProviderEnum.Fallback
					);
				}

				var sercurity = new SecurePayResponseDto
				{
					Transaction_Id = "SP-19283",
					Result = "success",
					NameProvider = result.NameProvider,
					Fee = result.Fee
				};

				return sercurity;
			}
			catch (HttpRequestException ex) when (IsServerError(ex))
			{
				var security = new SecurePayDto
				{
					Amount_Cents = Convert.ToInt32(request.Amount),
					Currency_Code = request.Currency,
					Client_Reference = "ORD-20251022"
				};

				return await _securePayProvider.ProcessAsync(
						security,
						(int)ProviderEnum.Fallback
					);
			}
		}

		private bool IsServerError(HttpRequestException ex)
		{
			if (ex.StatusCode == null)
				return false;

			int code = (int)ex.StatusCode.Value;
			return code >= 500 && code <= 530;
		}
	}
}
