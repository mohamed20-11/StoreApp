using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryManeger:MainManager<Category>
    {
        public CategoryManeger(MyDBContext myDBContext):base(myDBContext) { }    
    }
}
