﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
	}
}
