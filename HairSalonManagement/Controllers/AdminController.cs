using Microsoft.AspNetCore.Mvc;

namespace HairSalonManagement.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
