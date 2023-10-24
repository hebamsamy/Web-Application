using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class CartManager : MainManager<CartItem>
    {
        ProductManager productManager;
        public CartManager(MyDBContext myDBContext,ProductManager _productManager) : base(myDBContext)
        {
            productManager = _productManager;
        }
        public List<CartItemViewModel> Get(string UserId)
        {
            return GetList()
                .Where(i=>i.UserID==UserId)
                .Select(i => i.ToViewModel()).ToList();
        }
        public void Add(int ProductID, string UserID)
        {
            var prd = productManager.GetOneByID(ProductID);  
            var newcart = new CartItem()
            {
                ProductID = ProductID,
                UserID = UserID,
                Quantity = 1,
                SupPrice = prd.Price,
            };
            base.Add(newcart);
        }
        public void Update(int ProductID, string UserID, int newQty)
        {
            var data = GetList().Where(i=>i.ProductID == ProductID && i.UserID== UserID).FirstOrDefault();
            data.Quantity = newQty;
            var prd = productManager.GetOneByID(ProductID);
            data.SupPrice = prd.Price * newQty;
            base.Update(data);

        }
        public void Updateauth(int id, int newQty)
        {

        }

        public void Delete(int Id)
        {
            var oldprod = GetList().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);
        }
    }
}
