using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Web_Application.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Product List";
            var db = new MyDBContext();
            ViewData["User"] = "Heba";

            ViewData["Categories"] = db.Categories.Select(c => c.Name).ToList();
            
            List<Product> list=  db.Products.Include(i => i.ProductAttachments).Include(i=>i.Category).ToList();

            return View(list) ;
        }

        public IActionResult GetOne(int id)
        {

            var db = new MyDBContext();
            Product product = db.Products.Where(i => i.ID == id).Include(i => i.ProductAttachments).Include(i => i.Category).FirstOrDefault();
            ViewBag.Title = "Product "+ product.Name;
            return View(product);
        }

    }
}
