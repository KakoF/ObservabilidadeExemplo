using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Controllers
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
			_httpClient.BaseAddress = new Uri("https://localhost:1001/");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			Random random = new Random();
			int randomNumber = random.Next(0, 11);
			if(randomNumber == 4 || randomNumber == 8 || randomNumber == 10)
			{
				throw new Exception($"Random number is {randomNumber}");
			}
			/*HttpResponseMessage response = await _httpClient.GetAsync("Emprestimo");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception(await response.Content.ReadAsStringAsync());
			}

			string conteudo = await response.Content.ReadAsStringAsync();
			return Ok(conteudo);*/
			return Ok("conteudo");
		}
	}
}

