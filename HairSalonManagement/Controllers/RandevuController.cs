using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HairSalonManagement.Controllers
{
	public class RandevuController : Controller
	{
		private readonly ApplicationDbContext _context;

		public RandevuController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Randevu formunu görüntüle
		[HttpGet]
		public IActionResult CreateRandevu()
		{
			// Eğer kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Auth");
			}

			// Uzmanları ViewBag'e ekleyerek formda listelenmesini sağla
			var uzmanlar = _context.Uzmanlar.ToList(); // Uzmanlar veritabanından alınıyor
			ViewBag.Uzmanlar = uzmanlar;

			return View();
		}

		// Randevu kaydetme (POST)
		[HttpPost]
		public async Task<IActionResult> CreateRandevu(string isim, string soyisim, string telefon, string email, string uzmanIsim)
		{
			// Eğer kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Auth");
			}

			// Kullanıcıdan alınan verileri Randevu modeline ekle
			var randevu = new Randevu
			{
				Isim = isim,
				Soyisim = soyisim,
				Telefon = telefon,
				KullaniciEmail = email,  // Kullanıcı e-posta adresini ekliyoruz
				UzmanIsim = uzmanIsim,  // Seçilen uzman adı
				RandevuTarihi = DateTime.Now // Randevu tarihi
			};

			// Randevuyu veritabanına kaydet
			_context.Randevular.Add(randevu);
			await _context.SaveChangesAsync();  // Veritabanına kaydet

			// Başarılı kaydetme sonrası mesaj
			TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";
			return RedirectToAction("Index", "Home");
		}
		[HttpGet]
		public IActionResult Randevularim()
		{
			// Cookie'den kullanıcının e-posta adresini alıyoruz
			var kullaniciEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

			if (string.IsNullOrEmpty(kullaniciEmail))
			{
				// Eğer e-posta bilgisi yoksa, kullanıcıyı login sayfasına yönlendiriyoruz
				return RedirectToAction("Login", "Auth");
			}

			// Kullanıcıya ait randevuları alıyoruz
			var randevular = _context.Randevular
				.Where(r => r.KullaniciEmail == kullaniciEmail)
				.ToList();

			// Randevuları View'a gönderiyoruz
			return View(randevular);
		}


	}
}
