using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairSalonManagement.Controllers
{
	public class ServiceController : Controller
	{
		private readonly ApplicationDbContext _context;

		// Constructor injection
		public ServiceController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Tüm hizmetleri listele
		public IActionResult Index()
		{
			var services = _context.Services.ToList(); // Services tablosundaki tüm kayıtları al
			return View(services);
		}

		// Belirli bir hizmetin detaylarını getir
		public IActionResult HizmetDetay(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen geçerli bir hizmet ID'si giriniz.";
				return RedirectToAction("Index");
			}

			var service = _context.Services.FirstOrDefault(s => s.ServiceID == id);

			if (service is null)
			{
				TempData["msj"] = "Hizmet bulunamadı.";
				return RedirectToAction("Index");
			}

			return View(service);
		}

		// Yeni hizmet ekleme sayfasını aç
		public IActionResult HizmetEkle()
		{
			return View();
		}

		// Yeni hizmet ekleme işlemini kaydet
		[HttpPost]
		public IActionResult HizmetEkle(Service service)
		{
			if (ModelState.IsValid)
			{
				_context.Services.Add(service);
				_context.SaveChanges();
				TempData["msj"] = service.Name + " adlı hizmet başarıyla eklendi.";
				return RedirectToAction("Index");
			}

			TempData["msj"] = "Lütfen geçerli veriler giriniz.";
			return RedirectToAction("HizmetEkle");
		}

		// Mevcut bir hizmeti düzenleme sayfasını aç
		public IActionResult HizmetDüzenle(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen geçerli bir hizmet ID'si giriniz.";
				return RedirectToAction("Index");
			}

			var service = _context.Services.Find(id);
			if (service is null)
			{
				TempData["msj"] = "Hizmet bulunamadı.";
				return RedirectToAction("Index");
			}

			return View(service);
		}

		// Mevcut bir hizmetin düzenleme işlemini kaydet
		[HttpPost]
		public IActionResult HizmetDüzenle(int? id, Service service)
		{
			if (id is null || id != service.ServiceID)
			{
				TempData["msj"] = "ID eşleşmiyor.";
				return RedirectToAction("Index");
			}

			if (ModelState.IsValid)
			{
				_context.Services.Update(service);
				_context.SaveChanges();
				TempData["msj"] = service.Name + " adlı hizmet başarıyla güncellendi.";
				return RedirectToAction("Index");
			}

			TempData["msj"] = "Güncelleme işlemi başarısız oldu.";
			return RedirectToAction("HizmetDüzenle", new { id = id });
		}

		// Belirli bir hizmeti silme işlemi
		public IActionResult HizmetSil(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen geçerli bir hizmet ID'si giriniz.";
				return RedirectToAction("Index");
			}

			var service = _context.Services.Find(id);
			if (service is null)
			{
				TempData["msj"] = "Hizmet bulunamadı.";
				return RedirectToAction("Index");
			}

			_context.Services.Remove(service);
			_context.SaveChanges();
			TempData["msj"] = service.Name + " adlı hizmet başarıyla silindi.";
			return RedirectToAction("Index");
		}
	}
}
