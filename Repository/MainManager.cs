using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IQueryable<T> Get(
            Expression<Func<T,bool>> Filter,
            string OrderBy,
            bool IsAscending,
            int PageSize,
            int PageIndex)
        {
            var Quary= Set.AsQueryable();
            if (Filter != null) { 
                Quary = Quary.Where(Filter);
            }
            Quary = Quary.OrderBy(OrderBy,IsAscending);
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }
            //   3 =>    13/6=2.1
            var temp = Set.Count() / Convert.ToDouble(PageSize);
            if (PageIndex > temp +1)
            {
                PageIndex = 1;
            }
            int ToBeSkiped = (PageIndex - 1) * PageSize;
            return Quary.Skip(ToBeSkiped).Take(PageSize);
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
