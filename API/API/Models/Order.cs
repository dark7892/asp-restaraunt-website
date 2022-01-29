using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string PaymentMethod { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [NotMapped]
        public int[] DeletedOrderItemsIds { get; set; }

        //Foreign Key Sections

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderItems { get; set; }
    }
}
