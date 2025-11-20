namespace FastPay.Proxy.Application.Interface
{
	public interface IHandler<TRequest, TResponse>
	{
		Task<TResponse> HandleAsync(TRequest request);
	}
}
