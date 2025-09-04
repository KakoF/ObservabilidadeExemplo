using Core.Renegociacao.Meters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Core.Renegociacao.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmprestimoController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<EmprestimoController> _logger;
		private readonly MetricsHelper _metrics;

		public EmprestimoController(ILogger<EmprestimoController> logger, IHttpClientFactory httpClientFactory, MetricsHelper metrics)
		{
			_httpClient = httpClientFactory.CreateClient();
			//_httpClient.BaseAddress = new Uri("http://localhost:9000/api/");
			_httpClient.BaseAddress = new Uri("http://laravel-app:8000/api/");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			_logger = logger;
			_metrics = metrics;
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			try
			{
				Random random = new Random();
				int randomNumber = random.Next(0, 6);
				if (randomNumber == 1)
				{
					throw new Exception($"Random number is {randomNumber}");
				}

				// Counter com tags parametrizadas
				_metrics.RecordCounter(
				"emprestimos.core.processados",
				value: 1,
				new KeyValuePair<string, object>("status", "sucesso"),
				new KeyValuePair<string, object>("id", id),
				new KeyValuePair<string, object>("endpoint", $"Emprestimo{id}")
			);

				// Ou usando dicionário para tags
				var tags = new Dictionary<string, object>
				{
					["status"] = "sucesso",
					["id"] = id,
					["endpoint"] = $"Emprestimo{id}",
				};
				_metrics.RecordCounter("emprestimos.core.processados", 1, tags);
				return Ok(new { Result = "Core.Renegociacao" });

			}
			catch (Exception ex)
			{
				// Counter para erros
				_metrics.RecordCounter(
					"emprestimos.core.erros",
					value: 1,
					new KeyValuePair<string, object>("tipo_erro", ex.GetType().Name),
					new KeyValuePair<string, object>("id", id)
				);

				throw;
			}
			finally
			{
				stopwatch.Stop();

				// Histogram com tags parametrizadas
				_metrics.RecordHistogram(
					"tempo.processamento.emprestimos.core",
					stopwatch.ElapsedMilliseconds,
					new KeyValuePair<string, object>("endpoint", $"Emprestimo{id}"),
					new KeyValuePair<string, object>("id", id)
				);
			}

		}
	}
}
