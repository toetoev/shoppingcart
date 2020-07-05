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
    public class Customer
    {
        [Required]
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public Customer()
        {
        }

        public Customer(String username, String password)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = username;
            this.Password = AuthHash.GetSimpleHash(password);
        }
    }
}
