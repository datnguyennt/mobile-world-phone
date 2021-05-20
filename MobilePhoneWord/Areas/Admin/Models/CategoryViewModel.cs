using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobilePhoneWord.Areas.Admin.Models
{
	public class CategoryViewModel
	{
        [Key]
        public int CategoryID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public bool? Status { get; set; }
    }
}