using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;

public class RandevuController : Controller
{
	private readonly ApplicationDbContext _context;

	public RandevuController(ApplicationDbContext context)
	{
		_context = context;
	}

	// Randevu al sayfasına giriş yapmadan erişilemez
	public IActionResult Al()
	{
		var kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

		if (kullaniciId == null)
		{
			TempData["ErrorMessage"] = "Randevu almak için giriş yapmanız gerekmektedir.";
			return RedirectToAction("Login", "Auth");
		}

		return View();
	}
}
