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
		public List<User> ListAll()
		{

			return (from l in db.User
					select l).OrderBy(x => x.UserID).ToList();
		}

		public int Insert(User customer)
		{
			db.User.Add(customer);
			db.SaveChanges();
			return customer.UserID;
		}

		public bool Delete(int id)
		{
			try
			{
				var cus = db.User.Find(id);
				db.User.Remove(cus);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public bool ChangeStatus(int id)
		{
			var cus = db.User.Find(id);
			cus.Status = !cus.Status;
			db.SaveChanges();
			return cus.Status;
		}

	}
}
