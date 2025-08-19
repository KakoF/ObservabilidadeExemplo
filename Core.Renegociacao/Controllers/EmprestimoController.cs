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
			_httpClient.BaseAddress = new Uri("https://localhost:7075/");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			_logger = logger;
		}

		[HttpGet]
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			Random random = new Random();
			int randomNumber = random.Next(0, 11);
			if (randomNumber == 7)
			{
				throw new Exception($"Random number is {randomNumber}");
			}
			await Task.Delay(TimeSpan.FromSeconds(randomNumber));
			/*HttpResponseMessage response = await _httpClient.GetAsync("Emprestimo");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception(await response.Content.ReadAsStringAsync());
			}

			string conteudo = await response.Content.ReadAsStringAsync();
			return Ok(conteudo);*/
			return Ok(new { Result = "Core.Renegociacao" });
		}
	}
}
