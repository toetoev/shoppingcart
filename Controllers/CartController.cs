using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DB;
using ShoppingCart.Services;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        protected CartContext dbcontext;

        public CartController(CartContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if(username != null)
            {
                ViewBag.Auth = "true";
            }

            ViewData["session"] = username;
            return View();
        }

        public IActionResult PurchaseItem(string productlist, [FromServices] DBTester tester)
        {
            string username = HttpContext.Session.GetString("username");
            
            if(username == null)
            {           
                TempData["notuser"] = "true";
                return RedirectToAction("Login", "Home");
            }

            string strgroupids = productlist.Remove(productlist.Length - 1);
            string[] arraylist = strgroupids.Split(' ').ToArray();
            string uid = tester.GetUID(username);
            int count = tester.MakeOrder(uid);

            if (count >= 1)
            {
                int orderid = tester.GetOrderQuantity();

                foreach (var pair in arraylist)
                {
                    int[] vs = pair.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    int gettingid = vs[0];
                    int quantity = vs[1];

                    tester.InsertOrderDetail(gettingid, orderid, quantity);
                    do
                    {
                        count += tester.ProduceActivatCode(orderid, gettingid);
                        quantity--;
                    } while (quantity > 0);  
                }  
            }
            ViewData["session"] = username;
            return RedirectToAction("PurchaseHistory", "Cart");
        }

        public IActionResult PurchaseHistory([FromServices] DBTester tester)
        {
            ViewBag.Auth = "True";

            string username = HttpContext.Session.GetString("username");
            if(username == null)
            {
                TempData["notuser"] = "true";
                return RedirectToAction("Login", "Home");
            }

            string uid = tester.GetUID(username);

            List<Order> orders = tester.RetrievePurchase(uid);
            List<ProductList> products = new List<ProductList>();

            foreach (var order in orders)
            {
                int orderid = order.Id;
                DateTime date = order.OrderDate;
                
                List<OrderDetails> orderDetails = tester.GetOrderDetails(orderid);

                foreach (var od in orderDetails)
                {
                    int productid = od.ProductId;
                    int qty = od.Quantity;

                    List<Product> info = tester.GetProduct(productid);
                    List<string> code = tester.GetActivationCode(productid);
                    string name;
                    string image;
                    double price;

                    foreach (var pro in info)
                    {
                        name = pro.Name;
                        image = pro.Image;
                        price = pro.Price;

                        products.Add(new ProductList
                        {
                            OrderId = orderid,
                            ProductId = productid,
                            Name = name,
                            Price = price,
                            Quantity = qty,
                            Image = image,
                            PurchaseDate = date.ToString("MMMM dd, yyyy"),
                            ActivationCode = code
                        });
                    }
                }
            }

            ViewBag.Auth = "true";
            ViewData["session"] = username;
            ViewData["PurchaseHistory"] = products;
            return View();            
        }
    }
}