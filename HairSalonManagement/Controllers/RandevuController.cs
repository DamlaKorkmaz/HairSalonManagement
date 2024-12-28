using HairSalonManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public IActionResult CreateRandevu(Randevu model)
        {
            // Eğer kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                model.KullaniciEmail = User.Identity.Name; // Kullanıcının e-posta adresini ekle
                _context.Randevular.Add(model);
                _context.SaveChanges();

                // Başarılı kaydetme sonrası mesaj
                TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";
                return RedirectToAction("Randevularim");
            }

            // Eğer model geçerli değilse formu yeniden göster
            var uzmanlar = _context.Uzmanlar.ToList(); // Uzmanlar tekrar alınır
            ViewBag.Uzmanlar = uzmanlar;

            return View(model);
        }

        public IActionResult Randevularim()
        {
            // Kullanıcı kimliğini al
            var KullaniciEmail = User.Identity.Name;

            // Veritabanından kullanıcının randevularını filtrele
            var randevular = _context.Randevular
                .Where(r => r.KullaniciEmail == KullaniciEmail) // E-posta ile filtre
                .ToList();

            return View(randevular);
        }
    }
}
