using LinqKit;
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

        public PaginationViewModel<List<ProductVeiwModel>> Search(
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            int ProductID = 0,
            double Price = 0,
            string OrderBy = "ID",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex = 1
            )
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty( Name))
            {
                filter = filter.Or(i => i.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                filter = filter.Or(i => i.Category.Name.ToLower().Contains(CategoryName.ToLower()));
            }
            if(CategoryID != 0)
            {
                filter = filter.Or(i=>i.CategoryID == CategoryID);
            }
            if (ProductID != 0)
            {
                filter = filter.Or(i => i.ID == ProductID);
            }
            if (Price != 0)
            {
                filter = filter.And(i => i.Price <= Price);
            }
            if (oldFilter == filter)
            {
                filter = null;
            }
            var count = (filter!=null)? GetList().Where(filter).Count(): base.GetList().Count();
            var result =  Get(filter, OrderBy, IsAscending, PageSize, PageIndex);
            return new PaginationViewModel<List<ProductVeiwModel>>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                Count = count,
                Data = result.Select(i => i.ToVeiwModel()).ToList()
            };
        }
        public ProductVeiwModel GetOneByID(int id)
        {
            return Get().Where(i => i.ID == id).FirstOrDefault();
        }
        public AddProductViewModel GetEditableByID(int id)
        {
            return GetList().Where(i => i.ID == id).FirstOrDefault().ToAddViewModel();
        }
        public void Add(AddProductViewModel addProduct)
        {
            var temp = addProduct.ToModel();
            base.Add(temp);
        }

        public void Edit (AddProductViewModel newPrd)
        {
            var oldprod = GetList().Where(i => i.ID == newPrd.ID).FirstOrDefault();
            oldprod.Name = newPrd.Name;        
            oldprod.Price = newPrd.Price;        
            oldprod.Quantity = newPrd.Quantity;        
            oldprod.CategoryID = newPrd.CategoryID;
            oldprod.Description = newPrd.Description;
            if (newPrd.KeepImages == false)
            {
                oldprod.ProductAttachments.Clear();
            }
            oldprod.ProductAttachments = new List<ProductAttachment>();
            foreach (var item in newPrd.ImagesURL)
            {
                oldprod.ProductAttachments.Add(new ProductAttachment()
                {
                    Image = item
                });
            }

            Update(oldprod);
        }

        public void Delete(int Id)
        {
            var oldprod = GetList().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);

        }

        

    }
}
