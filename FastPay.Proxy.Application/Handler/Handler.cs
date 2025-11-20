using FastPay.Proxy.Application.Application;
using FastPay.Proxy.Application.Interface;
using FastPay.Proxy.Domain.Dto.Request;
using FastPay.Proxy.Domain.Dto.Response;
using Microsoft.Extensions.Configuration;

namespace FastPay.Proxy.Application.Handler
{
	public class Handler(IConfiguration configuration, IProxyFlow proxyFlow) : IHandler<PaymentRequestDto, PaymentStatusResponseDto>
	{
		private readonly IConfiguration _configuration = configuration;
		private readonly IProxyFlow _proxyFlow = proxyFlow;

		public async Task<PaymentStatusResponseDto> HandleAsync(PaymentRequestDto request)
		{
			var security = await _proxyFlow.FlowPayment(request);

			if (security.Result == "success")
			{
				return new PaymentStatusResponseDto
				{
					Id = 2,
					ExternalId = "",
					Status = "approved",
					Provider = security.NameProvider,
					GrossAmount = request.Amount,
					Fee = security.Fee,
					NetAmount = request.Amount - security.Fee
				};
			}

			return new PaymentStatusResponseDto
			{
				Id = 2,
				ExternalId = "",
				Status = "reject",
				Provider = security.NameProvider,
				GrossAmount = 0m,
				Fee = security.Fee,
				NetAmount = 0m
			};
		}
	}
}
