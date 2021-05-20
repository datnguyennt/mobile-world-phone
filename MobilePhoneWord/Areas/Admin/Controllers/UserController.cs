using MobilePhoneWord.Areas.Admin.Models;
using Models.DAO;
using Models.EF;
using PagedList;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class UserController : Controller
	{
		PhoneDbContext db = new PhoneDbContext();

		// GET: Admin/Customer
		public ActionResult Index()
		{
			return View(db.User.ToList());
		}

		[HttpGet]
		public JsonResult LoadCustomer()
		{
			try
			{
				var lstUser = (from u in db.User
							   select new UserViewModel
							   {
								   UserID = u.UserID,
								   Username = u.Username,
								   LastName = u.LastName,
								   FirstName = u.FirstName,
								   FullName = u.LastName + " " + u.FirstName,
								   Address = u.Address,
								   Email = u.Email,
								   Phone = u.Phone,
								   CreatedFor = u.CreatedFor,
								   Status = u.Status,
								   Password = u.Password
							   }).ToList();

				return Json(new {code = 200, lstUser = lstUser, msg = "Thành công"}, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Lấy danh sách người dùng thất bại"+ ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public JsonResult ChangeStatus(int id)
		{
			var result = new CustomerDAO().ChangeStatus(id);

			return Json(new
			{
				status = result
			});
		}

		[HttpPost]
		public JsonResult AddUser(string Username, string LastName, string FirstName, string Phone, string Email, string Address, string Password)
		{
			string pass_MD5 = Encryptor.MD5Hash(Password); //Mã hóa mật khẩu về MD5 trước khi truyền vào
			try
			{
				User user = new User();
				user.Username = Username;
				user.LastName = LastName;
				user.FirstName = FirstName;
				user.Phone = Phone;
				user.Email = Email;
				user.Address = Address;
				user.Password = pass_MD5;

				db.User.Add(user);
				db.SaveChanges(); //Luu vao csdl
				return Json(new { code = 200, msg = "Add success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Add error" +ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public JsonResult DetailUser(int idUser)
		{
			try
			{
				var user = db.User.SingleOrDefault(x => x.UserID == idUser);
				return Json(new { code = 200, User = user, msg="Success"}, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { code = 500, msg = "Fail" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult UpdateUser(int idUser, string Username, string LastName, string FirstName, string Phone, string Email, string Address, string Password)
		{
			//string pass_MD5 = Encryptor.MD5Hash(Password); //Mã hóa mật khẩu về MD5 trước khi truyền vào
			try
			{
				//Tìm user
				var user = db.User.SingleOrDefault(x => x.UserID == idUser);

				//Gán giá trị mới cho user
				user.Username = Username;
				user.LastName = LastName;
				user.FirstName = FirstName;
				user.Phone = Phone;
				user.Email = Email;
				user.Address = Address;
				user.Password = Password;

				db.SaveChanges(); //Luu vao csdl
				return Json(new { code = 200, msg = "Update success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Update error" + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}
	}


}