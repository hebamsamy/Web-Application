using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductManager: MainManager<Product>
    {
        public ProductManager(MyDBContext myDBContext) : base(myDBContext) { }

        public IQueryable<Product> Get()
        {
            return GetList();
        }

        public Product GetOneByID(int id)
        {
            return Get().Where(i => i.ID == id).FirstOrDefault();
        }

        public void Edit (Product newPrd ,int id)
        {
            var oldprod = GetOneByID(id);
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
            var oldprod = GetOneByID(Id);
            Delete(oldprod);
            /////

        }

        

    }
}
