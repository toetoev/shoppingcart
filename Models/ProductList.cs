using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class ProductList
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }
        public string Image { get; set; }
        public List<string> ActivationCode { get; set; }
        public string PurchaseDate { get; set; }
    }
}
