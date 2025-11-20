using FastPay.Proxy.Domain.Dto.Request;
using FastPay.Proxy.Domain.Dto.Response;

namespace FastPay.Proxy.Application.Interface
{
	public interface IProxyFlow
	{
		Task<SecurePayResponseDto> FlowPayment(PaymentRequestDto request, CancellationToken ct = default);
	}
}
