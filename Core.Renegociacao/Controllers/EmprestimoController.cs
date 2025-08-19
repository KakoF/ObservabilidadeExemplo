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

		public EmprestimoController(ILogger<EmprestimoController> logger, IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("http://localhost:8000/api/");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			_logger = logger;
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			Random random = new Random();
			int randomNumber = random.Next(0, 11);
			if (randomNumber == 7)
			{
				throw new Exception($"Random number is {randomNumber}");
			}
			await Task.Delay(TimeSpan.FromSeconds(randomNumber));
			HttpResponseMessage response = await _httpClient.GetAsync($"emprestimos/{id}");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception(await response.Content.ReadAsStringAsync());
			}

			var conteudo = await response.Content.ReadFromJsonAsync<object>();
			return Ok(conteudo);
			//return Ok(new { Result = "Core.Renegociacao" });
		}
	}
}
