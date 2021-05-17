using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class SupplyDAO
	{
		PhoneDbContext db = null;

		public SupplyDAO()
		{
			db = new PhoneDbContext();
		}
		public List<Suppliers> ListAll()
		{
			
			return (from l in db.Suppliers
					select l).OrderBy(x => x.Id).ToList();
		}



		public int Insert(Suppliers supply)
		{
			db.Suppliers.Add(supply);
			db.SaveChanges();
			return supply.Id;
		}
	}
}
