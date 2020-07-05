using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using ShoppingCart.DB;


namespace ShoppingCart.Models
{
    public class Product
    {
        [Required]
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Image { get; set; }

        public Product()
        {
        }

        public Product(int id,string name, string desc, double price, string image)
        {
            this.Id = id;
            this.Name = name;
            this.Description = desc;
            this.Price = price;
            this.Image = image;
        }
    }
}
