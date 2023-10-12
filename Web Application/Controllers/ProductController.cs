using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Web_Application.Models;
using ViewModel;

namespace Web_Application.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager;
        CategoryManeger categoryManeger;
        UnitOfWork unitOfWork;

        [ViewData]
        public UserDataModel UserData { get; set; }
        public ProductController(ProductManager _productManager,
            CategoryManeger _categoryManeger,
            UnitOfWork _unitOfWork
            )
        {
            unitOfWork = _unitOfWork;
            productManager = _productManager;       
            categoryManeger = _categoryManeger;

            UserData = new UserDataModel()
            {
                Id = 1,
                Name = "Heba",
                picture="6.jpg"
            };
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Product List";
            ViewData["User"] = "Heba";

            ViewData["Categories"] = categoryManeger.GetList().Select(i=>i.Name).ToList();
            
            List<ProductVeiwModel> list=  productManager.Get().ToList();

            return View(list) ;
        }

        //[Route("/details/{id}")]
        public IActionResult GetOne(int id, string name = "")
        {
            ProductVeiwModel product = productManager.GetOneByID(id);
            ViewBag.Title = "Product "+ product.Name;
            return View(product);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Categories"] = GetCategories();
            ViewBag.Title = "Add Product ";
            return View();
        }
        [HttpPost]
        
        public IActionResult Add(AddProductViewModel addProduct)
        {
          if(ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),"Content","Images",file.FileName), 
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImagesURL.Add(file.FileName);
                }

                

                productManager.Add(addProduct);
                unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["Categories"] = GetCategories();
                return View(); 
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = categoryManeger.GetList().ToList();
            ProductVeiwModel product = productManager.GetOneByID(id);
            ViewBag.Title = "Edit Product " + product.Name;
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(AddProductViewModel addProduct)
        {
            Product prd = new Product();
            prd.ID = addProduct.ID;
            prd.Name = addProduct.Name;
            prd.Price = addProduct.Price;
            prd.Quantity = addProduct.Quantity;
            prd.Description = addProduct.Description;
            prd.CategoryID = addProduct.CategoryID;
            productManager.Update(prd);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProductVeiwModel product = productManager.GetOneByID(id);
            productManager.Delete(product.ToModel());
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private List<SelectListItem > GetCategories()
        {
          return   categoryManeger.GetList().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ID.ToString(),
            }).ToList();
        }



    }
}
