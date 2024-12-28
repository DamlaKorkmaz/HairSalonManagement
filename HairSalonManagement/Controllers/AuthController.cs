using HairSalonManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HairSalonManagement.Controllers
{
	public class AuthController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AuthController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Register sayfasını göstermek için GET
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// Kullanıcı kaydı (POST)
		[HttpPost]
		public async Task<IActionResult> Register(Kullanici kullanici)
		{
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurun.";
				return View(kullanici);
			}

			try
			{
				await _context.Kullanicilar.AddAsync(kullanici);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Kaydınız başarıyla tamamlandı!";
				return RedirectToAction("Login");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
				return View(kullanici);
			}
		}

		// Giriş sayfası (GET)
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// Kullanıcı girişi (POST)
		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
			var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Email == email);

			if (kullanici != null && kullanici.Sifre == password)
			{
				// Claims oluştur
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, kullanici.Isim),
					new Claim(ClaimTypes.Email, kullanici.Email),
					new Claim("KullaniciId", kullanici.KullaniciId.ToString())
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// Oturum başlat
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return RedirectToAction("Index", "Home");
			}
			
			ViewBag.Error = "Geçersiz email veya şifre.";
			return View();
		}

		// Çıkış işlemi
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
