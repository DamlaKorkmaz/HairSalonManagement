using NewHairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace New.Controllers
{
	public class EmployeeController : Controller
	{
		SalonContext k = new SalonContext();
		public IActionResult Index()
		{
			var employee = k.Çalışanlar.ToList();
			return View(employee);
		}
		public IActionResult EmployeeDetails(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen dataları düzgün giriniz";
				return RedirectToAction("Index");
			}
			var employee = k.Çalışanlar.Include(x => x.Hizmetler).First(x => x.EmployeeID == id);
			if (employee is null)
			{
				TempData["msj"] = "Çalışan Bulunamadı";
				return RedirectToAction("Index");
			}
			return View(employee);
		}

		public IActionResult Employeeadd()
		{
			return View();
		}

		public IActionResult EmployeeDelete(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen dataları düzgün giriniz";
				return RedirectToAction("Index");
			}
			var employee = k.Çalışanlar.Find(id);
			if (employee is null)
			{
				TempData["msj"] = "Çalışan Bulunamadı";
				return RedirectToAction("Index");
			}
			var kayit = k.Çalışanlar.Include(x => x.Hizmetler).Where(x => x.EmployeeID == id).ToList();
			if (kayit[0].Hizmetler.Count > 0)
			{
				TempData["msj"] = "Yazara ait Kitaplar var. Önce Kitapları siliniz";
				return RedirectToAction("Index");
			}
			k.Çalışanlar.Remove(employee);
			k.SaveChanges();
			TempData["msj"] = employee.FirstName + " adlı yazar silindi";
			return RedirectToAction("Index");
		}
		public IActionResult EmployeeSave(Employee y)
		{
			if (ModelState.IsValid)
			{
				k.Çalışanlar.Add(y);
				//  k.Add(y);
				k.SaveChanges();
				TempData["msj"] = y.FirstName + " adlı çalışan eklendi";
				return RedirectToAction("Index");
			}
			TempData["msj"] = "Lütfen Dataları düzgün giriniz";
			return RedirectToAction("Employeeadd");
		}

		public IActionResult Employeefix(int? id)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen dataları düzgün giriniz";
				return RedirectToAction("Index");
			}
			var employee = k.Çalışanlar.Find(id);
			if (employee is null)
			{
				TempData["msj"] = "Yazar Bulunamadı";
				return RedirectToAction("Index");
			}
			return View(employee);
		}
		[HttpPost]
		public IActionResult Employeefix(int? id, Employee y)
		{
			if (id is null)
			{
				TempData["msj"] = "Lütfen dataları düzgün giriniz";
				return RedirectToAction("Index");
			}
			if (id != y.EmployeeID)
			{
				TempData["msj"] = "id ler eşleşmiyor";
				return RedirectToAction("Index");
			}

			if (ModelState.IsValid)
			{
				k.Çalışanlar.Update(y);
				k.SaveChanges();
				TempData["msj"] = y.FirstName + "adlı yazara ait güncelleme yapıldı";
				return RedirectToAction("Index");
			}
			TempData["msj"] = "Güncelleme işlemi başarısız";
			return RedirectToAction("Index");
		}
	}
}
