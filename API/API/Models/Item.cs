using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")] //Means that decimal can have 18 digits with 2 decimal points.
        public decimal Price { get; set; }
    }
}
