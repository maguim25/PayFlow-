using FastPay.Proxy.Domain.Dto;
using FastPay.Proxy.Domain.Dto.Response;

namespace FastPay.Proxy.Application.Interface
{
	public interface ISecurePayProvider
	{
		Task<SecurePayResponseDto> ProcessAsync(SecurePayDto securePayDto, int providerId = 0, CancellationToken ct = default);
	}
}
