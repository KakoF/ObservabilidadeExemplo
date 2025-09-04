using BFF.Meters;

namespace BFF.Middleware
{
	public class MetricsMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly AppMetrics _metrics;

		public MetricsMiddleware(RequestDelegate next, AppMetrics metrics)
		{
			_next = next;
			_metrics = metrics;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			try
			{
				await _next(context);

				// Registra métricas baseadas no status code
				var statusCode = context.Response.StatusCode;
				var statusType = statusCode switch
				{
					>= 200 and < 300 => "sucesso",
					>= 400 and < 500 => "erro_cliente",
					>= 500 => "erro_servidor",
					_ => "outro"
				};

				_metrics.RegistrarRequisicao($"http_{statusType}");
			}
			finally
			{
				stopwatch.Stop();
				_metrics.RegistrarTempoProcessamento(
					stopwatch.ElapsedMilliseconds,
					context.Request.Path);
			}
		}
	}

}
