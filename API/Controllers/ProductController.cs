using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModel;

namespace API.Controllers
{
    public class ProductController : ControllerBase
    {
        ProductManager productManager;
        CategoryManeger categoryManeger;
        UnitOfWork unitOfWork;
        public ProductController(ProductManager _productManager, CategoryManeger _categoryManeger, UnitOfWork _unitOfWork)
        {
            this.productManager = _productManager;
            this.categoryManeger = _categoryManeger;
            this.unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            var list = productManager.Get();
            return new ObjectResult(list);
        }
        
        public IActionResult Search(
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            int ProductID = 0,
            double Price = 0,
            string OrderBy = "Price",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex = 1) {
            var list = productManager.Search(Name,CategoryName,CategoryID,ProductID,Price,OrderBy,IsAscending,PageSize,PageIndex);
            return new ObjectResult(list);
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Vendor")]
        public IActionResult Add([FromForm]AddProductViewModel addProduct)
        {
            var user = User;
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImagesURL.Add(file.FileName);
                }



                productManager.Add(addProduct);
                unitOfWork.Commit();
                return Ok();

            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }

                return new ObjectResult(str);
            }
        }


    }
}
