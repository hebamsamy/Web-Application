using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ViewModel;
using Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    [Authorize(Roles ="User")]
    public class CartController : ControllerBase
    {
        UnitOfWork unitOfWork;
        CartManager cartManager;
        public CartController(CartManager _cartManager,UnitOfWork _unitOfWork) {
            cartManager = _cartManager;
            unitOfWork = _unitOfWork;
        }
        public IActionResult Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = cartManager.Get(userId);
            return new JsonResult(new ApiResultVewModel<List<CartItemViewModel>>()
            {
                data= data,
                success= true,
                massage ="Get Successfully",
                status = 200
            });
        }
        [HttpPost("Cart/Add/{PrdID}")]
        public IActionResult Add(int PrdID) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cartManager.Add(PrdID, userId);
            unitOfWork.Commit();
            return new JsonResult(new ApiResultVewModel<int>()
            {
                data = 0,
                success = true,
                massage = "Get Successfully",
                status = 200
            });
        }

    }
}
