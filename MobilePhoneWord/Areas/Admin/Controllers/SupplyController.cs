using Models;
using Models.DAO;
using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class SupplyController : Controller
	{
		PhoneDbContext context = new PhoneDbContext();

		// GET: Admin/Supply
		public ActionResult Index(int? page)
		{
			// 1. Tham số int? dùng để thể hiện null và kiểu int
			// page có thể có giá trị là null và kiểu int.

			// 2. Nếu page = null thì đặt lại là 1.
			if (page == null)
				page = 1;
			// 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
			int pageSize = 3;

			// 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
			// nếu page = null thì lấy giá trị 1 cho biến pageNumber.
			int pageNumber = (page ?? 1);

			var dao = new SupplyDAO();
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
		public ActionResult Create(Suppliers model, HttpPostedFileBase imageSave)
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
				Suppliers supp = new Suppliers
				{
					Logo = imageSave.FileName
				};
				model.Logo = supp.Logo;
				context.Suppliers.Add(model);
				context.SaveChanges();//Lưu lại
				return RedirectToAction("Index", "Supply");
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
			var supply = context.Suppliers.Find(id);

			return View(supply);
		}

		// GET: Supp/Edit
		public ActionResult Edit(int id)
		{
			var context = new PhoneDbContext();
			var supply = context.Suppliers.Find(id);

			return View(supply);
		}

		[HttpPost]
		public ActionResult Edit(Suppliers model, HttpPostedFileBase imageSave)
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

				Suppliers supp = new Suppliers
				{
					Logo = imageSave.FileName
				};

				var context = new PhoneDbContext();
				var old = context.Suppliers.Find(model.Id);

				old.SupplyCode = model.SupplyCode;
				old.Name = model.Name;
				old.Phone = model.Phone;
				old.Email = model.Email;
				old.Logo = supp.Logo;
				//old.SinhVien.HoTen = model.SinhVien.HoTen;

				context.SaveChanges();
				return RedirectToAction("Index");

			}
			else
			{
				return View();
			}
		}
	}
}