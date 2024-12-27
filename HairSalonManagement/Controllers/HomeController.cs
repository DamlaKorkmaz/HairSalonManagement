using System.Diagnostics;
using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalonManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			return View();
		}
		// Randevu Alma Sayfası (Giriş yapmamış kullanıcı erişemez)
		public IActionResult Randevu()
		{
			var userRole = HttpContext.Session.GetString("UserRole");

			if (string.IsNullOrEmpty(userRole))
			{
				return RedirectToAction("Login", "Auth");
			}

			return View(); // Randevu sayfasını göster
		}

		// Kullanıcıyı yönlendiren sayfa
		public IActionResult Dashboard()
		{
			var userRole = HttpContext.Session.GetString("UserRole");

			if (string.IsNullOrEmpty(userRole))
			{
				return View(); // Genel kullanıcı sayfası
			}

			if (userRole == "Admin")
			{
				return RedirectToAction("AdminPanel", "Admin");
			}
			else if (userRole == "User")
			{
				return View(); // Kullanıcı panelini göster
			}

			return RedirectToAction("Login", "Auth");
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
