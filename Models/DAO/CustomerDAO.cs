using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class CustomerDAO
	{
		PhoneDbContext db = null;

		public CustomerDAO()
		{
			db = new PhoneDbContext();
		}
		public List<KhachHang> ListAll()
		{

			return (from l in db.KhachHang
					select l).OrderBy(x => x.KHID).ToList();
		}

		public int Insert(KhachHang customer)
		{
			db.KhachHang.Add(customer);
			db.SaveChanges();
			return customer.KHID;
		}

		public bool Delete(int id)
		{
			try
			{
				var cus = db.KhachHang.Find(id);
				db.KhachHang.Remove(cus);
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
