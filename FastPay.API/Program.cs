
using FastPay.Proxy.Application.Application;
using FastPay.Proxy.Application.Handler;
using FastPay.Proxy.Application.Interface;
using FastPay.Proxy.Domain.Dto.Request;
using FastPay.Proxy.Domain.Dto.Response;

namespace FastPay.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<ISecurePayProvider, SecurePayProvider>();
			builder.Services.AddScoped<IProxyFlow, ProxyFlow>();
			builder.Services.AddScoped<IHandler<PaymentRequestDto, PaymentStatusResponseDto>, Handler>();



			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
