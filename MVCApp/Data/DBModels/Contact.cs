﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    public partial class Contact
    {
        [Key]
        [Column("ContactID")]
        public int ContactId { get; set; }
        [StringLength(50)]
        public string ContactType { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string ContactName { get; set; }
        [StringLength(30)]
        public string ContactTitle { get; set; }
        [StringLength(60)]
        public string Address { get; set; }
        [StringLength(15)]
        public string City { get; set; }
        [StringLength(15)]
        public string Region { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        [StringLength(15)]
        public string Country { get; set; }
        [StringLength(24)]
        public string Phone { get; set; }
        [StringLength(4)]
        public string Extension { get; set; }
        [StringLength(24)]
        public string Fax { get; set; }
        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
    }
}
