using Finance.Core.DTOs.Result;
using Finance.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Finance.API.Middlewares
{
	public static class UseCustomExceptionHandler
	{
		public static void UserCustomException(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(config =>
			{
				config.Run(async context => // Eğer Run fonksiyonuna girerse buradan ileriye gitmez ve geriye döner
				{
					context.Response.ContentType = "application/json"; // Response olarak karşı tarafa ne döneceğimi belirtir

					var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); // Hatanın detaylarını alır

					var statusCode = exceptionFeature.Error switch
					{
						ClientSideException => 400,
						NotFoundException => 404,
						_ => 500
					};

					context.Response.StatusCode = statusCode;

					var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

					await context.Response.WriteAsync(JsonSerializer.Serialize(response));

				});
			});
		}
	}
}
