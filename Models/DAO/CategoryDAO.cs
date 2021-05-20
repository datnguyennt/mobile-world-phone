using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class CategoryDAO
	{
		PhoneDbContext db = null;

		public CategoryDAO()
		{
			db = new PhoneDbContext();
		}
		public List<ProductCategory> ListAll()
		{

			return (from l in db.ProductCategory
					select l).OrderBy(x => x.CategoryID).ToList();
		}



		public int Insert(ProductCategory cat)
		{
			db.ProductCategory.Add(cat);
			db.SaveChanges();
			return cat.CategoryID;
		}

		public bool Delete(int id)
		{
			try
			{
				var supp = db.ProductCategory.Find(id);
				db.ProductCategory.Remove(supp);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{

				return false;
			}

		}
	}
}
