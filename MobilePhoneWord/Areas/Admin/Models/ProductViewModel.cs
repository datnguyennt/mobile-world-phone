using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobilePhoneWord.Areas.Admin.Models
{
	public class ProductViewModel
	{
        public int ProductID { get; set; }

        [StringLength(10)]
        public string ProductCode { get; set; }

        [StringLength(250)]
        public string ProductName { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string ProductImage { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public int? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool? Status { get; set; }

        public int? ViewCounts { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}