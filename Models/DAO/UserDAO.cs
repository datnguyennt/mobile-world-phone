using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class UserDAO
	{
		PhoneDbContext db = null;
        public UserDAO()
        {
            db = new PhoneDbContext();
        }

        //Đăng nhập
        public int Login(String UserName, String UserPassword)
        {
            var result = db.User.SingleOrDefault(x => x.Username == UserName);
            if (result == null) //Nếu không tồn tại username thì return 0
            {
                return 0;
            }
            else
            {
                if (result.Status == false) //Trả về -1 nếu status của tài khoản đang ở trạng thái false
                {
                    return -1;
                }
                else
                {
                    if (result.Password == UserPassword)
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
        public User getUserByID(string user)
        {
            return db.User.SingleOrDefault(x => x.Username.Contains(user));
        }
    }
}
