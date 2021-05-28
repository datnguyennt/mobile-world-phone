using MobilePhoneWord.Areas.Admin.Models;
using Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneWord.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		PhoneDbContext db = new PhoneDbContext();

		// GET: Admin/Porduct
		public ActionResult Index()
		{
			//return View(db.Products.ToList());
			return View();
		}

		
		public ActionResult getCategoryName()
		{
			var lstCategory = db.ProductCategory.Select(x => new
			{
				ID = x.CategoryID,
				Name = x.Name
			}).ToList();
			return Json(new { code = 200, lstCategory = lstCategory, msg = "Thành công" }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult getStatus()
		{
			var lstStatus = db.Products.Select(x => new
			{
				ID = x.ProductID,
				Status = x.Status
			}).ToList();
			return Json(new { code = 200, lstStatus = lstStatus, msg = "Thành công" }, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		public JsonResult LoadProduct(string tukhoa, int trang)
		{
			try
			{
				int pageSize = 6;

				var lstProduct = (from p in db.Products
								  join c in db.ProductCategory on p.CategoryID equals c.CategoryID
								  where p.ProductName.ToLower().Contains(tukhoa) || c.Name.ToLower().Contains(tukhoa)
								  select new ProductViewModel
								  {
									  
									  ProductID = p.ProductID,
									  ProductCode = p.ProductCode,
									  ProductName = p.ProductName,
									  CategoryName = c.Name,
									  Price = p.Price,
									  Quantity = p.Quantity,
									  ViewCounts = p.ViewCounts,
									  Status = p.Status
								  }).ToList();
				
				var soTrang = lstProduct.Count() % pageSize == 0 ? lstProduct.Count()/pageSize : lstProduct.Count() /pageSize + 1;
				var pageHienThi = lstProduct.Skip((trang - 1) * pageSize)
								  .Take(pageSize).ToList();
				return Json(new { code = 200, soTrang = soTrang, lstProduct = pageHienThi, msg = "Thành công" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Lấy danh sách san pham thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
			}
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
		[HttpGet]
		public ActionResult AddProduct()
		{
			return View();
		}

		[HttpPost]
		public JsonResult AddProduct(Products model, HttpPostedFileBase imageSave)
		{
			if (ModelState.IsValid)
			{
				// TODO: Add insert logic here
				var context = new PhoneDbContext();

				if (imageSave.ContentLength > 0)
				{
					string imageFileName = Path.GetFileName(imageSave.FileName);
					string folderPath = Path.Combine(Server.MapPath("~/Assets/Admin/images/product"), imageFileName);
					imageSave.SaveAs(folderPath);
				}

				//Khởi tạo lớp sinh viên để thêm trước
				Products supp = new Products
				{
					//ProductCode = model.ProductCode,
					//ProductName = model.ProductName,
					//Price = model.Price,
					//MetaTitle = model.MetaTitle,
					//Quantity = model.Quantity,
					//Status = model.Status,
					//CategoryID = model.CategoryID,
					ProductImage = imageSave.FileName
				};
				model.ProductImage = supp.ProductImage;

				context.Products.Add(model);
				context.SaveChanges();//Lưu lại
				return Json(new { code = 200, msg = "Update success" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new { code = 500, msg = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public JsonResult DetailProduct(int idProduct)
		{
			try
			{

				db.Configuration.ProxyCreationEnabled = false;
				var product = db.Products.SingleOrDefault(x => x.ProductID == idProduct);
				return Json(new { code = 200, Product = product, msg = "Success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception e)
			{
				return Json(new { code = 500, msg = "Fail" + e}, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult UpdateProduct(Products p)
		{
			try
			{
				//Tìm user
				var products = db.Products.SingleOrDefault(x => x.ProductID == p.ProductID);
				
			
				//Gán giá trị mới cho user
				//products.ProductID = p.ProductID;
				products.ProductName = p.ProductName;
				products.Price = p.Price;
				products.CategoryID = p.CategoryID;
				products.Status = p.Status;
				products.Quantity = p.Quantity;
				products.ProductCode = p.ProductCode;
				products.MetaTitle = p.MetaTitle;
				db.SaveChanges(); //Luu vao csdl
				return Json(new { code = 200, msg = "Update success" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { code = 500, msg = "Update error" + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult deleteproduct(int id)
		{
			try
			{
				var result = db.Products.Find(id);
				db.Products.Remove(result);
				db.SaveChanges();
				return this.Json(new { code = 200, msg = "Delete Success" }, JsonRequestBehavior.AllowGet);

			}
			catch (Exception e)
			{
				return this.Json(new { code = 500, msg = "Fail"+ e }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}