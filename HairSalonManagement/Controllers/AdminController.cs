using Microsoft.AspNetCore.Mvc;
using HairSalonManagement.Models;
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
