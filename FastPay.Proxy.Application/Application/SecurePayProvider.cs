using FastPay.Proxy.Application.Interface;
using FastPay.Proxy.Domain.Dto;
using FastPay.Proxy.Domain.Dto.Response;
using FastPay.Proxy.Domain.Enum;
using Newtonsoft.Json;
using System.Text;


namespace FastPay.Proxy.Application.Application
{
	public class SecurePayProvider : ISecurePayProvider
	{
		public async Task<SecurePayResponseDto> ProcessAsync(SecurePayDto securePayDto, int providerId = 0, CancellationToken ct = default)
		{
			var provider = await GetProvider((int)ProviderEnum.Default);

			if (providerId == (int)ProviderEnum.Fallback)
			{
				provider = await GetProvider((int)ProviderEnum.Fallback);
			}

			var newFee = await CalculateFee(securePayDto.Amount_Cents, provider);

			await Task.Delay(30, ct); 

			//bypass
			if (false)
			{
				var result = await PostAsync(provider.Url, new
				{
					amount = securePayDto.Amount_Cents,
					typeCurency = securePayDto.Currency_Code,
				});
			}

			var security = new SecurePayResponseDto
			{
				Transaction_Id = "SP-19283",
				Result = "success",
				Fee = newFee,
				NameProvider = provider.Name
			};

			return security;
		}

		private async Task<ProviderDto> GetProvider(int providerId = 0)
		{
			switch (providerId)
			{
				case (int)ProviderEnum.Default:
					return new ProviderDto { Name = "FastPay", Url = "xxxxxx" } ;
				case (int)ProviderEnum.Fallback:
					return new ProviderDto { Name = "SecurePay", Url = "xxxxxx2" };
				default:
					break;
			}
			return new ProviderDto { };
		}

		private async Task<decimal> CalculateFee(decimal amount, ProviderDto provider)
		{
			if (provider == null || string.IsNullOrWhiteSpace(provider.Name))
				return 0m;

			return provider.Name switch
			{
				"FastPay" => amount * 0.0349m,
				"SecurePay" => amount * 0.0299m + 0.40m,
				_ => 0m
			};
		}

		private async Task<PaymentStatusResponseDto> PostAsync(string url, object payload)
		{
			using var httpClient = new HttpClient();

			using var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

			using var response = await httpClient.PostAsync(url, content);

			var responseBody = await response.Content.ReadAsStringAsync();
			var newJson = JsonConvert.DeserializeObject<PaymentStatusResponseDto>(responseBody);

			return newJson;
		}
	}
}
