using MobilePhoneWord.Areas.Admin.Models;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class CategoryController : Controller
	{
		PhoneDbContext db = new PhoneDbContext();
		public int pageSize = 2;
		// GET: Admin/Category
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public JsonResult LoadCustomer(string search)
		{
			try
			{
				db.Configuration.ProxyCreationEnabled = false;
				var result = (from u in db.ProductCategory
							  select new
							  {
								  CategoryID = u.CategoryID,
								  Name = u.Name,
								  MetaTitle = u.MetaTitle,
								  Status = u.Status
							  });

				if (!String.IsNullOrEmpty(search))
				{
					result = result.Where(s => s.Name.Contains(search));
				}

				return this.Json(new { code = 200, lstCategory = result }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Lấy danh sách người dùng thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult AddCategory(CategoryViewModel model)
		{
			try
			{
				var category = new ProductCategory
				{
					Name = model.Name,
					MetaTitle = model.MetaTitle
				};
				db.ProductCategory.Add(category);
				db.SaveChanges();
				return this.Json(new { code = 200, msg = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return this.Json(new { code = 500, msg = "Lấy danh sách người dùng thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public JsonResult DetailCategory(int idCategory)
		{
			try
			{
				db.Configuration.ProxyCreationEnabled = false;
				var cat = db.ProductCategory.SingleOrDefault(x => x.CategoryID == idCategory);
				return this.Json(new { code = 200, Category = cat, msg = "Success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return this.Json(new { code = 500, msg = "Fail" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult UpdateCategory(CategoryViewModel model)
		{
			try
			{
				//Tìm lớp dựa vào ID
				db.Configuration.ProxyCreationEnabled = false;
				var result = db.ProductCategory.SingleOrDefault(x => x.CategoryID == model.CategoryID);

				result.Name = model.Name;
				result.MetaTitle = model.MetaTitle;
				db.SaveChanges();
				return this.Json(new { code = 200, msg = "Update Success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return this.Json(new { code = 500, msg = "Fail" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult DeleteCategory(int id)
		{
			try
			{
				db.Configuration.ProxyCreationEnabled = false;
				var result = db.ProductCategory.Find(id);
				db.ProductCategory.Remove(result);
				db.SaveChanges();
				return this.Json(new { code = 200, msg = "Delete Success" }, JsonRequestBehavior.AllowGet);

			}
			catch (Exception)
			{
				return this.Json(new { code = 500, msg = "Fail" }, JsonRequestBehavior.AllowGet);
			}
		}

	}
}