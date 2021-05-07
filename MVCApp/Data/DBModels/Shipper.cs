using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("ShipperID")]
        public int ShipperId { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(24)]
        public string Phone { get; set; }
        [Column("RegionID")]
        public int? RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        [InverseProperty("Shippers")]
        public virtual Region Region { get; set; }
        [InverseProperty(nameof(Order.ShipViaNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
