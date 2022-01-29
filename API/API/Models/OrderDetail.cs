using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        //Foreign Key
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

    }
}
