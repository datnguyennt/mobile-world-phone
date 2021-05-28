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
			return View(db.NhanVien.ToList());
		}

		[HttpGet]
		public JsonResult LoadEmployee()
		{
			try
			{
				var lstEmployyee = (from u in db.NhanVien
							   select new NhanVienViewModel
							   {
								  NVID = u.NVID,
								   UserName = u.UserName,
								   FullName = u.FullName,
								   NVAddress = u.NVAddress,
								   Email = u.Email,
								   PhoneNum = u.PhoneNum,
								   Quyen = u.Quyen,
								   Active = u.Active,
								   NVPassword = u.NVPassword
							   }).ToList();

				return Json(new {code = 200, lstUser = lstEmployyee, msg = "Thành công"}, JsonRequestBehavior.AllowGet);
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

		//[HttpPost]
		//public JsonResult ChangeStatus(int id)
		//{
		//	var result = new CustomerDAO().ChangeStatus(id);

		//	return Json(new
		//	{
		//		status = result
		//	});
		//}

		[HttpPost]
		public JsonResult AddEmployee(NhanVienViewModel model)
		{
			string pass_MD5 = Encryptor.MD5Hash(model.NVPassword); //Mã hóa mật khẩu về MD5 trước khi truyền vào
			try
			{
				NhanVien user = new NhanVien();
				user.UserName = model.UserName;
				user.FullName = model.UserName;
				user.PhoneNum = model.PhoneNum;
				user.Email = model.Email;
				user.NVAddress = model.NVAddress;
				user.Quyen = model.Quyen;
				user.NVPassword = pass_MD5;

				db.NhanVien.Add(user);
				db.SaveChanges(); //Luu vao csdl
				return Json(new { code = 200, msg = "Add success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Add error" +ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public JsonResult DetailEmployee(int idUser)
		{
			try
			{
				var user = db.NhanVien.SingleOrDefault(x => x.NVID == idUser);
				return Json(new { code = 200, User = user, msg="Success"}, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { code = 500, msg = "Fail" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult UpdateEmployee(NhanVienViewModel model)
		{
			string pass_MD5 = Encryptor.MD5Hash(model.NVPassword); //Mã hóa mật khẩu về MD5 trước khi truyền vào
			try
			{
				//Tìm user
				var user = db.NhanVien.SingleOrDefault(x => x.NVID == model.NVID);

				//Gán giá trị mới cho user
				user.UserName = model.UserName;
				user.FullName = model.UserName;
				user.PhoneNum = model.PhoneNum;
				user.Email = model.Email;
				user.NVAddress = model.NVAddress;
				user.Quyen = model.Quyen;
				user.NVPassword = pass_MD5;

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