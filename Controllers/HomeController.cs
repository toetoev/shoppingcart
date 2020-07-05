using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.DB;
using Microsoft.AspNetCore.Http;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        protected CartContext dbcontext;

        public HomeController(CartContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index([FromServices] DBTester tester)
        {
            
            ViewData["session"] = HttpContext.Session.GetString("username"); 
            ViewData["ProductData"] = tester.RetrieveProduct();

            string username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Auth = "true";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, [FromServices] DBTester tester)
        {
            Customer cu = tester.ValidCustomer(userName);
            password = AuthHash.GetSimpleHash(password);

     
            if (cu == null)
            {
                TempData["ErrorMessage"] = "true";
                return View();
            }

            if (cu.Password == password)
            {
                HttpContext.Session.SetString("username", userName);
                return RedirectToAction("Index");
            }

            else
            {
                TempData["ErrorMessage"] = "true";
                return View();
            }
        }    
        
        public IActionResult Search([FromServices] DBTester tester, string keyword = "")
        { 
            string username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Auth = "true";
            }

            List<Product> products = tester.Search(keyword);
            ViewData["ProductData"] = products;
            ViewData["session"] = HttpContext.Session.GetString("username");
            ViewData["Searchkeyword"] = keyword;

            return View();
        }

        public IActionResult Logout(string cmd)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ProductDetails([FromServices] DBTester tester, int productid)
        {

            ViewData["session"] = HttpContext.Session.GetString("username");
            Product product = tester.ProductComment(productid);
            List<Review> reviews = tester.Getallreviews(productid);
            string username = HttpContext.Session.GetString("username");

            if (username != null)
            {
                ViewBag.Auth = "true";
            }
            ViewData["session"] = username;
            ViewData["ProductData"] = product;
            ViewData["GetReviews"] = reviews;
            return View();
        }

        public IActionResult SaveComments([FromServices] DBTester tester, int productid, string comment)
        {          

            string username = HttpContext.Session.GetString("username");
            ViewData["ProductData"] = tester.ProductComment(productid);

            ViewBag.Auth = "true";
            string userid = tester.GetUID(username);
            tester.CreateReviews(productid, userid, comment);
            
            ViewData["GetReview"] = tester.Getallreviews(productid);
            //return RedirectToAction("ProductDetails", "Home");
            return RedirectToAction("ProductDetails", "Home", new { @productid = productid });

        }
    }
}
