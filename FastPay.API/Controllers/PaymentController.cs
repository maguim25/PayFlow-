using FastPay.Proxy.Application.Interface;
using FastPay.Proxy.Domain.Dto.Request;
using FastPay.Proxy.Domain.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace FastPay.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly ILogger<PaymentController> _logger;
		private readonly IHandler<PaymentRequestDto, PaymentStatusResponseDto> _handler;

		public PaymentController(ILogger<PaymentController> logger, IHandler<PaymentRequestDto, PaymentStatusResponseDto> handler)
		{
			_logger = logger;
			_handler = handler;
		}

		[HttpPost]
		public async Task<IActionResult> Payment([FromBody] PaymentRequestDto payment)
		{
			// depois você tipa esse `t` como seu RequestDto
			var result = await _handler.HandleAsync(payment);
			return Ok(result);
		}
	}
}
