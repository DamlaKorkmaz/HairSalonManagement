using Microsoft.AspNetCore.Mvc;

namespace HairSalonManagement.Controllers
{
	public class ServiceController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
