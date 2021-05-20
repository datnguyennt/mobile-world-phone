using MobilePhoneWord.Areas.Admin.Models;
using MobilePhoneWord.Extensions;
using Models.DAO;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class LoginController : Controller
	{
		// GET: Admin/Login
		public ActionResult Index()
		{
			//var session = Session[Constants.USER_SEESION]; //Tạo biến session kiểm tra xem có tồn tại hay không
			//if (session != null)
			//{
			//	return RedirectToAction("Index", "Home"); //Trả về trang chủ index nếu đã tồn tại session (đã đăng nhập thành công rồi nhưng lại trỏ url về lại trang login ấy)
			//}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //Sử dụng cái này để chống giả mạo request 
		public ActionResult Index(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDAO(); //Khởi tạo constructor User

				string pass_MD5 = Encryptor.MD5Hash(model.UserPassword); //Mã hóa mật khẩu về MD5 trước khi truyền vào

				var result = dao.Login(model.Username, pass_MD5); //Tạo biến result để kiểm tra đăng nhập
				if (result == 1) //Nếu tên đăng nhập và mật khẩu đúng
				{
					//tạo biến user để lấy thông tin cần thiết, sau đó truyền vào session
					var user = dao.getUserByID(model.Username);

					var userSession = new UserLogin
					{
						UserName = user.Username,
						UserID = user.Id,
						FirstName = user.Fullname
					};
					Session.Add(Constants.USER_SEESION, userSession); //Session này để kiểm tra khi vào trang chủ index home
					Session["FullName"] = user.Fullname.ToString(); //Session này dùng cho lời chào ở trang index home
					return RedirectToAction("Index", "Home"); //Sau đó quay về trang index home
				}
				else if (result == 0) //Nếu tài khoản không tồn tại
				{
					this.AddNotification("Tài khoản không tồn tại", NotificationType.ERROR);
				}
				else if (result == -1)
				{
					this.AddNotification("Tài khoản đang bị khóa", NotificationType.ERROR);
				}
				else if (result == -2)
				{
					this.AddNotification("Sai mật khẩu", NotificationType.ERROR);

				}
				else
				{
					this.AddNotification("Sai tên đăng nhập", NotificationType.ERROR);
				}
			}
			return View("Index"); //Nếu modelstate == false thì ở yên tại chỗ
		}
	}
}