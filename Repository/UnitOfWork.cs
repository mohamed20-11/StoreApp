using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork
    {
        private MyDBContext MyDBContext { get; set; }
        public UnitOfWork(MyDBContext myDB) {
            MyDBContext = myDB;
        }
        public void Commit() {
            MyDBContext.SaveChanges();
        }
    }
}
