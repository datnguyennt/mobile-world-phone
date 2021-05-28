using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class EmployeeDAO
	{
        PhoneDbContext db = null;
        public EmployeeDAO()
        {
            db = new PhoneDbContext();
        }

        //Đăng nhập
        public int Login(String UserName, String UserPassword)
        {
            var result = db.NhanVien.SingleOrDefault(x => x.UserName == UserName);
            if (result == null) //Nếu không tồn tại username thì return 0
            {
                return 0;
            }
            else
            {
                if (result.Active == false) //Trả về -1 nếu status của tài khoản đang ở trạng thái false
                {
                    return -1;
                }
                else
                {
                    if (result.NVPassword == UserPassword)
                    {
                        return 1; //Trả về 1 nếu tên đăng nhập và mật khẩu đều đúng
                    }
                    else
                    {
                        return -2; //Trả về -2 nếu đúng tên đăng nhập nhưng sai mật khẩu
                    }
                }
            }
        }
        public NhanVien getUserByID(string user)
        {
            return db.NhanVien.SingleOrDefault(x => x.UserName.Contains(user));
        }
    }
}
