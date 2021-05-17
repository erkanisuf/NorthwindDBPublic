using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int LoginId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string PassWord { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
