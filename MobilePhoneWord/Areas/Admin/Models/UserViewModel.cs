using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobilePhoneWord.Areas.Admin.Models
{
	public class UserViewModel
	{
        public int UserID { get; set; }

        [StringLength(10)]
        public string Username { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

		public string FullName { get; set; }

		[StringLength(250)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? CreatedFor { get; set; }

        public bool Status { get; set; }
    }
}