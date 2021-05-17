using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class BaseController : Controller
	{
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var session = (UserLogin)Session[Constants.USER_SEESION];
			if (session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Login", action = "Index", Areas = "Login" }));
			}
			base.OnActionExecuted(filterContext);
		}
	}
}