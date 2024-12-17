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

        public IActionResult Index()
        {
            var services = _context.Services.ToList(); // Veritabanındaki Services tablosundan verileri al
            return View(services);
        }
    }
}
