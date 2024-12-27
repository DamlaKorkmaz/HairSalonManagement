using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		// Admin oturum kontrolü
		if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
		{
			context.Result = RedirectToAction("Login", "Auth");
		}
		base.OnActionExecuting(context);
	}
}
