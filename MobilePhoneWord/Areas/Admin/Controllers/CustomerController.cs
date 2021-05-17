using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class CustomerController : Controller
	{
		// GET: Admin/Customer
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}
	}
}