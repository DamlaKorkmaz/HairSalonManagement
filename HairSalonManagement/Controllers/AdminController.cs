using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairSalonManagement.Controllers
{
	public class AdminController : BaseController
	{
		private readonly ApplicationDbContext _context;

		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Admin ana sayfası
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		// Uzmanları listeleme sayfası
		[HttpGet]
		public async Task<IActionResult> ManageExperts()
		{
			var uzmanlar = await _context.Uzmanlar.ToListAsync();
			return View(uzmanlar);
		}

		// Uzmanları listeleme sayfası
		[HttpGet]
		public async Task<IActionResult> ManageUsers()
		{
			var kullanici = await _context.Kullanicilar.ToListAsync();
			return View(kullanici);
		}

		// Uzman ekleme sayfasını göster
		[HttpGet]
		public IActionResult AddUzman()
		{
			return View();
		}

		// Uzman ekleme işlemi
		[HttpPost]
		public async Task<IActionResult> AddUzman(Uzman uzman)
		{
			if (!ModelState.IsValid)  // Model validasyonu
			{
				TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurduğunuzdan emin olun.";
				return View(uzman);
			}

			try
			{
				await _context.Uzmanlar.AddAsync(uzman); // Asenkron ekleme
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Yeni uzman başarıyla eklendi!";
				return RedirectToAction("ManageExperts");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
				return View(uzman);
			}
		}

		// Uzman silme işlemi
		[HttpGet]
		public async Task<IActionResult> DeleteUzman(int id)
		{
			var uzman = await _context.Uzmanlar.FindAsync(id);
			if (uzman == null)
			{
				TempData["ErrorMessage"] = "Uzman bulunamadı.";
				return RedirectToAction("ManageExperts");
			}

			_context.Uzmanlar.Remove(uzman);
			await _context.SaveChangesAsync();
			TempData["SuccessMessage"] = "Uzman başarıyla silindi.";
			return RedirectToAction("ManageExperts");
		}

		// Uzman düzenleme sayfasını göster
		[HttpGet]
		public async Task<IActionResult> EditUzman(int id)
		{
			var uzman = await _context.Uzmanlar.FindAsync(id);
			if (uzman == null)
			{
				TempData["ErrorMessage"] = "Uzman bulunamadı.";
				return RedirectToAction("ManageExperts");
			}

			return View(uzman);
		}

		// Uzman düzenleme işlemi
		[HttpPost]
		public async Task<IActionResult> EditUzman(int id, Uzman uzman)
		{
			if (id != uzman.UzmanId)
			{
				TempData["ErrorMessage"] = "Uzman ID'leri eşleşmiyor.";
				return RedirectToAction("ManageExperts");
			}

			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Geçersiz veri. Lütfen formu kontrol edin.";
				return View(uzman);
			}

			try
			{
				_context.Uzmanlar.Update(uzman);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Uzman başarıyla güncellendi.";
				return RedirectToAction("ManageExperts");
			}
			catch (DbUpdateConcurrencyException)
			{
				TempData["ErrorMessage"] = "Bir hata oluştu, lütfen tekrar deneyin.";
				return RedirectToAction("ManageExperts");
			}
		}
	}
}
