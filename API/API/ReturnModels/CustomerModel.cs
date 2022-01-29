using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.ReturnModels
{
    public class CustomerModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
