using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using HairSalonManagement.Models;
using System.Diagnostics;

namespace HairSalonManagement.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		// DeepAI API anahtarınız
		private readonly string apiKey = "4e7d9d07-5eaa-4ba9-870a-df582dc6c87a";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		// Ana sayfa için Index metodu
		public IActionResult Index(List<string> hairstyles = null)
		{
			// Saç modellerini ana sayfada göstermek için ViewBag kullanıyoruz
			ViewBag.Hairstyles = hairstyles;
			return View();
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
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		// Fotoğraf yükleme işlemi
		[HttpPost]
		public async Task<IActionResult> UploadPhoto(IFormFile photo)
		{
			if (photo != null && photo.Length > 0)
			{
				// Fotoğrafı kaydet
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await photo.CopyToAsync(stream);
				}

				// Fotoğrafı AI'ye gönder ve saç modellerini al
				var hairstyles = await GetHairstylesFromAI(filePath);

				// Saç modellerini ana sayfaya gönder
				return RedirectToAction("Index", new { hairstyles = hairstyles });
			}

			return View("Error");
		}

		private async Task<List<string>> GetHairstylesFromAI(string filePath)
		{
			var apiKey = "4e7d9d07-5eaa-4ba9-870a-df582dc6c87a";  // Verdiğiniz API anahtarını buraya ekledik
			var url = "https://api.deepai.org/api/image-recognition";
			var client = new HttpClient();

			// Fotoğrafı yükle
			var form = new MultipartFormDataContent();
			var fileBytes = System.IO.File.ReadAllBytes(filePath);
			var byteArrayContent = new ByteArrayContent(fileBytes);
			form.Add(byteArrayContent, "image", "image.jpg");

			// API anahtarını başlık olarak ekle
			client.DefaultRequestHeaders.Add("api-key", apiKey);

			// API'ye gönder
			var response = await client.PostAsync(url, form);
			var responseContent = await response.Content.ReadAsStringAsync();

			// API yanıtını yazdır
			Console.WriteLine("API Yanıtı: " + responseContent);

			// API yanıtını kontrol et
			if (string.IsNullOrEmpty(responseContent))
			{
				// Yanıt boş veya null geldiğinde hata fırlatılabilir
				throw new Exception("API yanıtı boş");
			}

			// AI tarafından dönen cevabı işleyin
			dynamic result = JsonConvert.DeserializeObject(responseContent);

			// API yanıtını kontrol edin
			if (result?.output == null)
			{
				throw new Exception("API yanıtında 'output' bulunamadı. Yanıtın içeriği: " + responseContent);
			}

			// Saç modelleriyle ilgili bilgileri çıkartın
			List<string> hairstyles = new List<string>();
			foreach (var tag in result.output)
			{
				hairstyles.Add(tag.ToString());
			}

			return hairstyles;
		}

	}
}
