using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class ProductManager: MainManager<Product>
    {
        public ProductManager(MyDBContext myDBContext) : base(myDBContext) { }

        public List<ProductVeiwModel> Get()
        {
            return GetList().Select(i=> i.ToVeiwModel()).ToList();
        }

        public ProductVeiwModel GetOneByID(int id)
        {
            return Get().Where(i => i.ID == id).FirstOrDefault();
        }
        public void Add(AddProductViewModel addProduct)
        {
            var temp = addProduct.ToModel();
            base.Add(temp);
        }

        public void Edit (Product newPrd ,int id)
        {
            var oldprod = GetList().Where(i => i.ID == id).FirstOrDefault();
            oldprod.Name = newPrd.Name;        
            oldprod.Price = newPrd.Price;        
            oldprod.Quantity = newPrd.Quantity;        
            oldprod.CategoryID = newPrd.CategoryID;
            oldprod.Description = newPrd.Description;

            Update(oldprod);
            ////
        }

        public void Delete(int Id)
        {
            var oldprod = GetList().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);
            /////

        }

        

    }
}
