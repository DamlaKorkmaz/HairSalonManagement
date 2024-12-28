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

		private const string AdminEmail = "b221210098@sakarya.edu.tr";
		private const string AdminPassword = "sau";

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
				// Kullanıcı zaten var mı kontrol et
				var mevcutKullanici = _context.Kullanicilar.FirstOrDefault(k => k.Email == kullanici.Email);
				if (mevcutKullanici != null)
				{
					TempData["ErrorMessage"] = "Bu email ile kayıtlı bir kullanıcı zaten var.";
					return View(kullanici);
				}

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

		// Giriş sayfası (GET ve POST)
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
			// Eğer admin email ve şifre girilmişse
			if (email == AdminEmail && password == AdminPassword)
			{
				// Admin için claims oluştur
				var adminClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, "Admin"),
			new Claim(ClaimTypes.Email, AdminEmail),
			new Claim(ClaimTypes.Role, "Admin") // Admin rolü
        };

				var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
				var adminPrincipal = new ClaimsPrincipal(adminIdentity);

				// Oturum başlat
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrincipal);

				// Admin paneline yönlendir
				return RedirectToAction("Index", "Admin");
			}

			// Normal kullanıcı kontrolü
			var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Email == email);

			if (kullanici != null && kullanici.Sifre == password)
			{
				// Kullanıcı için claims oluştur
				var userClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, kullanici.Isim),
			new Claim(ClaimTypes.Email, kullanici.Email),
			new Claim("KullaniciId", kullanici.KullaniciId.ToString())
		};

				var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
				var userPrincipal = new ClaimsPrincipal(userIdentity);

				// Oturum başlat
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

				return RedirectToAction("Index", "Home");
			}

			// Geçersiz giriş durumu
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
