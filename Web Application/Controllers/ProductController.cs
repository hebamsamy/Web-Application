using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Web_Application.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var db = new MyDBContext();
            List<Product> list=  db.Products.Include(i => i.ProductAttachments).Include(i=>i.Category).ToList();
            return View(list);
        }

        public IActionResult GetOne(int id)
        {
            var db = new MyDBContext();
            Product product = db.Products.Where(i => i.ID == id).Include(i => i.ProductAttachments).Include(i => i.Category).FirstOrDefault();
            return View(product);
        }

    }
}
