using Models.DAO;
using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class CustomerController : Controller
	{
		PhoneDbContext context = new PhoneDbContext();

		// GET: Admin/Customer
		public ActionResult Index(int? page)
		{
			// 1. Tham số int? dùng để thể hiện null và kiểu int
			// page có thể có giá trị là null và kiểu int.

			// 2. Nếu page = null thì đặt lại là 1.
			if (page == null)
				page = 1;
			// 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
			int pageSize = 4;

			// 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
			// nếu page = null thì lấy giá trị 1 cho biến pageNumber.
			int pageNumber = (page ?? 1);

			var dao = new CustomerDAO();
			var model = dao.ListAll();
			return View(model.ToPagedList(pageNumber, pageSize));
		}

		// GET: SinhVien/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SinhVien/Create
		//Thêm sinh viên, xử lý thêm ở phần POST
		[HttpPost]
		public ActionResult Create(Customers model, HttpPostedFileBase imageSave)
		{
			if (ModelState.IsValid)
			{
				// TODO: Add insert logic here
				var context = new PhoneDbContext();

				if (imageSave.ContentLength > 0)
				{
					string imageFileName = Path.GetFileName(imageSave.FileName);
					string folderPath = Path.Combine(Server.MapPath("~/Assets/Admin/img"), imageFileName);
					imageSave.SaveAs(folderPath);
				}

				//Khởi tạo lớp sinh viên để thêm trước
				Customers cus = new Customers
				{
					Photo = imageSave.FileName
				};
				model.Photo = cus.Photo;
				context.Customers.Add(model);
				context.SaveChanges();//Lưu lại
				return RedirectToAction("Index", "Customer");
			}
			else
			{
				return View();
			}
		}

		// GET: SinhVien/Details/5
		public ActionResult Details(int id)
		{
			var context = new PhoneDbContext();
			var cus = context.Customers.Find(id);

			return View(cus);
		}

		// GET: Supp/Edit
		public ActionResult Edit(int id)
		{
			var context = new PhoneDbContext();
			var cus = context.Customers.Find(id);

			return View(cus);
		}

		[HttpPost]
		public ActionResult Edit(Customers model, HttpPostedFileBase imageSave)
		{
			if (ModelState.IsValid)
			{
				// TODO: Add insert logic here
				if (imageSave.ContentLength > 0)
				{
					string imageFileName = Path.GetFileName(imageSave.FileName);
					string folderPath = Path.Combine(Server.MapPath("~/Assets/Admin/img"), imageFileName);
					imageSave.SaveAs(folderPath);
				}

				Customers cus = new Customers
				{
					Photo = imageSave.FileName
				};

				var context = new PhoneDbContext();
				var old = context.Customers.Find(model.Id);

				old.Fullname = model.Fullname;
				old.DOB = model.DOB;
				old.Activated = model.Activated;
				old.Email = model.Email;
				old.Photo = cus.Photo;
				old.UserPassword = cus.UserPassword;
				//old.SinhVien.HoTen = model.SinhVien.HoTen;

				context.SaveChanges();
				return RedirectToAction("Index");

			}
			else
			{
				return View();
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			CustomerDAO dao = new CustomerDAO();
			dao.Delete(id);
			return RedirectToAction("Index");
		}
	public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}
	}
}