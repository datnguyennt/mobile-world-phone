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
		public List<Customers> ListAll()
		{

			return (from l in db.Customers
					select l).OrderBy(x => x.Id).ToList();
		}

		public int Insert(Customers customer)
		{
			db.Customers.Add(customer);
			db.SaveChanges();
			return customer.Id;
		}

		public bool Delete(int id)
		{
			try
			{
				var cus = db.Customers.Find(id);
				db.Customers.Remove(cus);
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
