using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;
using ShoppingCart.DB;
using Microsoft.AspNetCore.Http;

namespace ShoppingCart.Services
{
    public class DBTester
    {
        protected CartContext dbcontext;

        public DBTester(CartContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public Customer ValidCustomer(string username)
        {
            Customer customer = dbcontext.Customers.Where( 
                x => x.Name == username).FirstOrDefault();
            return customer;
        }

        public List<Product> RetrieveProduct()
        {
            List<Product> products = dbcontext.Products.ToList();
            return products;
        }

        public List<Product> Search(string keyword)
        {
            
            if(keyword == null)
            {
                return dbcontext.Products.ToList();
            }

            var text = keyword.ToLower();
            return dbcontext.Products.Where(
                x => x.Name.ToLower().Contains(text) || x.Description.ToLower().Contains(text)
            ).ToList();
        }
         
        public string GetUID(string name)
        {
            return dbcontext.Customers.Where(x => x.Name == name).Select(y => y.Id).FirstOrDefault();
        }

        public int MakeOrder(string userid)
        {
            Order order = new Order();
            order.CustomerId = userid;
            order.OrderDate = DateTime.Now;
            dbcontext.Add(order);
            int count = dbcontext.SaveChanges();
            return count;
        }

        public int GetOrderQuantity()
        {

            int max = dbcontext.Orders.Max(x => x.Id);

            return max;
        }

        public int ProduceActivatCode(int orderqty, int productid)
        {
            ProductCode code = new ProductCode();
            code.ActivateCode = Guid.NewGuid().ToString();
            code.OrderID = orderqty;
            code.ProductID = productid;
            dbcontext.Add(code);
            int count = dbcontext.SaveChanges();
            return count;
        }

        public void InsertOrderDetail(int productid, int orderid, int qty)
        {
            OrderDetails od = new OrderDetails();
            od.ProductId = productid;
            od.OrderId = orderid;
            od.Quantity = qty;
            dbcontext.Add(od);
            dbcontext.SaveChanges();
        }

        public List<Product> GetProduct(int productid)
        {
            return dbcontext.Products.Where(p => p.Id == productid).ToList();
        }

        public List<Order> RetrievePurchase(string uid)
        {
            return dbcontext.Orders.Where(x => x.CustomerId == uid).ToList();
        }

        public List<OrderDetails> GetOrderDetails(int orderid)
        {
            return dbcontext.OrderDetails.Where(x => x.OrderId == orderid).ToList();
        }

        public List<string> GetActivationCode(int productid)
        {
            return dbcontext.ProductCodes.Where(x => x.ProductID == productid)
                            .Select(y => y.ActivateCode).ToList();
        }

        public Product ProductComment(int productid)
        {
            return dbcontext.Products.Where(x => x.Id == productid).FirstOrDefault();
        }

        public void CreateReviews(int productid, string userid, string comment)
        {
            Review review = new Review();
            review.ProductID = productid;
            review.CustomerId = userid;
            review.Comment = comment;
            review.GivenDate = DateTime.Now;

            dbcontext.Add(review);
            dbcontext.SaveChanges();
        }

        public List<Review> Getallreviews(int productid)
        {
            return dbcontext.Reviews.Where(x => x.ProductID == productid).ToList();
        }
    }
}
