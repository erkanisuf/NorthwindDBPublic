using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCApp.Data
{
    [Table("Documentation")]
    public partial class Documentation
    {
        [Key]
        [Column("DocumentationID")]
        public int DocumentationId { get; set; }
        [StringLength(200)]
        public string AvailableRoute { get; set; }
        [StringLength(10)]
        public string Method { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(10)]
        public string Keycode { get; set; }
    }
}
