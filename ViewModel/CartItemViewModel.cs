using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CartItemViewModel
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public List<string> ProductImages { get; set; }
        public int Quantity { get; set; }
        public double SupPrice { get; set; }
    }
    public static class CartEx
    {
        public static CartItemViewModel ToViewModel(this CartItem cart)
        {
            return new CartItemViewModel
            {
                ID = cart.ID,
                UserId = cart.UserID,
                ProductId = cart.ProductID,
                ProductName = cart.Product.Name,
                ProductPrice = cart.Product.Price,
                Quantity = cart.Quantity,
                SupPrice = cart.Quantity * cart.Product.Price,
                ProductImages = cart.Product.ProductAttachments.Select(x => x.Image).ToList(),

            };
        }
    }
}
