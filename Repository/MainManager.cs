using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MainManager<T> where T : class
    {
        private readonly MyDBContext dBContext;
        public readonly DbSet<T> Set;
        public MainManager(MyDBContext myDBContext)
        {
            /////Error
            dBContext = myDBContext;
            Set = dBContext.Set<T>();
        }

        public IQueryable<T> GetList()
        {
            return Set.AsQueryable();
        }
        public EntityEntry<T> Add(T Entry) =>
            Set.Add(Entry);

        public EntityEntry<T> Update(T Entry) =>
             Set.Update(Entry);
        public EntityEntry<T> Delete(T Entry) =>
            Set.Remove(Entry);



    }
}
