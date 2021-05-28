using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobilePhoneWord.Areas.Admin.Models
{
	public class NhanVienViewModel
	{
        public int NVID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string NVPassword { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(11)]
        public string PhoneNum { get; set; }

        public DateTime DOB { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public byte? Quyen { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(255)]
        public string NVAddress { get; set; }

        public bool? Active { get; set; }
    }
}