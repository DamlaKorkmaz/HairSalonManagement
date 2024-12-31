using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HairSalonManagement.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string openAiApiKey = "YOUR_API_KEY"; // OpenAI API anahtarınızı buraya koyun.
		private readonly string openAiApiUrl = "https://api.openai.com/v1/chat/completions";

		public HomeController()
		{
			_httpClient = new HttpClient();
		}

		public IActionResult Index(string aiResult = null)
		{
			ViewBag.AIResult = aiResult;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetSuggestion(string userDescription)
		{
			// OpenAI chat formatında istek verileri
			var requestPayload = new
			{
				model = "gpt-3.5-turbo", // Kullanılacak model
				messages = new[]
				{
					new { role = "system", content = "Sen bir saç stili önerme asistanısın." },
					new { role = "user", content = $"Kullanıcı açıklaması: {userDescription}. Bu açıklamaya uygun saç modeli önerisi yap." }
				},
				max_tokens = 100,
				temperature = 0.7
			};

			var content = new StringContent(JsonConvert.SerializeObject(requestPayload), Encoding.UTF8, "application/json");
			if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
			{
				_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");
			}

			var response = await _httpClient.PostAsync(openAiApiUrl, content);
			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				return RedirectToAction("Index", new { aiResult = $"Bir hata oluştu: {response.StatusCode} - {errorContent}" });
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

			// Yanıt içeriğini alıyoruz
			string suggestion = result.choices[0].message.content;
			return RedirectToAction("Index", new { aiResult = suggestion });
		}

		public IActionResult Hakkımızda()
		{
			return View();
		}

		public IActionResult Hizmetlerimiz()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}
