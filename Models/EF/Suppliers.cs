namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mã hãng điện thoại")]
        public string SupplyCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Tên hãng")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Logo thương hiệu")]
        public string Logo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Địa chỉ Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Số điện thoại")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
