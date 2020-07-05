using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class Order
    {
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
