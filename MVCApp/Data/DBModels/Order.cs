using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    [Index(nameof(CustomerId), Name = "CustomerID")]
    [Index(nameof(CustomerId), Name = "CustomersOrders")]
    [Index(nameof(EmployeeId), Name = "EmployeeID")]
    [Index(nameof(EmployeeId), Name = "EmployeesOrders")]
    [Index(nameof(OrderDate), Name = "OrderDate")]
    [Index(nameof(ShipPostalCode), Name = "ShipPostalCode")]
    [Index(nameof(ShippedDate), Name = "ShippedDate")]
    [Index(nameof(ShipVia), Name = "ShippersOrders")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }
        [Column("CustomerID")]
        [StringLength(5)]
        [Required]
        public string CustomerId { get; set; }
        [Column("EmployeeID")]
        [Required]
        public int? EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        
        [Required]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        [Required]
        public DateTime? RequiredDate { get; set; }
        [Column(TypeName = "datetime")]
        
        public DateTime? ShippedDate { get; set; }
        
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal? Freight { get; set; }
        [StringLength(40,MinimumLength =4,ErrorMessage = "Minimum Length of 4chars")]

        [Required]
        public string ShipName { get; set; }
        [StringLength(60)]

        [Required]
        public string ShipAddress { get; set; }
        [StringLength(15)]

        [Required]
        public string ShipCity { get; set; }
        [StringLength(15)]

        
        public string ShipRegion { get; set; }
        [StringLength(10)]

        
        public string ShipPostalCode { get; set; }
        [StringLength(15)]

        [Required]
        public string ShipCountry { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Orders")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(ShipVia))]
        [InverseProperty(nameof(Shipper.Orders))]
        public virtual Shipper ShipViaNavigation { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
