using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace HairSalonManagement.Controllers
{
	public class AuthController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AuthController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Kullanıcı Giriş Sayfası
		public IActionResult Login()
		{
			return View();
		}

		// Kullanıcı Giriş İşlemi
		[HttpPost]
		public IActionResult Login(string email, string sifre)
		{
			// Kullanıcı doğrulama
			var user = _context.Kullanicilar.FirstOrDefault(k => k.Email == email && k.Sifre == sifre);

			// Eğer kullanıcı bulunursa
			if (user != null)
			{
				// Oturum bilgilerini kaydet (Session)
				HttpContext.Session.SetString("UserEmail", email);
				HttpContext.Session.SetString("UserRole", "User"); // Varsayılan olarak normal kullanıcı

				// Admin doğrulaması
				if (email == "admin@gmail.com" && sifre == "sau")
				{
					HttpContext.Session.SetString("UserRole", "Admin");
				}

				// Kullanıcıyı ilgili panele yönlendir
				return RedirectToAction("Index", "Home");
			}

			// Eğer giriş başarısız olursa hata mesajı
			TempData["ErrorMessage"] = "E-posta veya şifre hatalı.";
			return View();
		}

		// Çıkış Yapma İşlemi
		public IActionResult Logout()
		{
			// Session temizleme (oturum kapatma)
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
