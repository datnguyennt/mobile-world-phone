namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [Key]
        public int MaDH { get; set; }

        public int? MaKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayMua { get; set; }

        public bool? Status { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
