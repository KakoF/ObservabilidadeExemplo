using BFF.Meters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BFF.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmprestimoController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<EmprestimoController> _logger;
		private readonly AppMetrics _metrics;

		public EmprestimoController(ILogger<EmprestimoController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, AppMetrics metrics)
		{
			_httpClient = httpClientFactory.CreateClient();
			//_httpClient.BaseAddress = new Uri("http://localhost:5204/");
			//_httpClient.BaseAddress = new Uri("http://core:8080/");
			_httpClient.BaseAddress = new Uri(configuration["Clients:Core"]!);
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
				if (randomNumber == 5)
				{
					throw new Exception($"Random number is {randomNumber}");
				}
				HttpResponseMessage response = await _httpClient.GetAsync($"Emprestimo/{id}");

				if (!response.IsSuccessStatusCode)
				{
					throw new Exception(await response.Content.ReadAsStringAsync());
				}

				var conteudo = await response.Content.ReadFromJsonAsync<object>();
				// Registra métricas
				_metrics.IncrementarUsuariosAtivos();

				return Ok(conteudo);
			}
			finally
			{
				stopwatch.Stop();
				_metrics.RegistrarTempoProcessamento(stopwatch.ElapsedMilliseconds, $"Emprestimo/{id}");
			}
		}
	}
}
