using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    public partial class Login
    {
        [Key]
        public int LoginId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string PassWord { get; set; }
    }
}
