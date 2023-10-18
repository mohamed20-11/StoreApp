using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Web_Application.Models;
using ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Web_Application.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        ProductManager productManager;
        CategoryManeger categoryManeger;
        UnitOfWork unitOfWork;

        [ViewData]
        public UserDataModel UserData { get; set; }
        public ProductController(ProductManager _productManager,
            CategoryManeger _categoryManeger,
            UnitOfWork _unitOfWork
            )
        {
            unitOfWork = _unitOfWork;
            productManager = _productManager;       
            categoryManeger = _categoryManeger;

            UserData = new UserDataModel()
            {
                Id = 1,
                Name = "Heba",
                picture="6.jpg"
            };
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Product List";
            ViewData["User"] = "Heba";

            ViewData["Categories"] = categoryManeger.GetList().Select(i=>i.Name).ToList();
            
            List<ProductVeiwModel> list=  productManager.Get().ToList();

            return View(list) ;
        }

        public IActionResult Search(
            int ID = 0,
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            double Price = 0,
            string OrderBy ="Price",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex= 1
            )
        {
            ViewData["Categories"] = GetCategories();
            var data = productManager.Search(Name,CategoryName,CategoryID,ID,Price,OrderBy,IsAscending,PageSize,PageIndex);
            return View(data);
        }
        //[Route("/details/{id}")]
        public IActionResult GetOne(int id, string name = "")
        {
            ProductVeiwModel product = productManager.GetOneByID(id);
            ViewBag.Title = "Product "+ product.Name;
            return View(product);
        }
        [Authorize(Roles ="Admin,Vendor")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Categories"] = GetCategories();
            ViewBag.Title = "Add Product ";
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Add(AddProductViewModel addProduct)
        {
          if(ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),"Content","Images",file.FileName), 
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImagesURL.Add(file.FileName);
                }

                

                productManager.Add(addProduct);
                unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["Categories"] = GetCategories();
                return View(); 
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = GetCategories();
            AddProductViewModel product = productManager.GetEditableByID(id);
            ViewBag.Title = "Edit Product " + product.Name;
            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Edit(AddProductViewModel addProduct)
        {
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImagesURL.Add(file.FileName);
                }
                productManager.Edit(addProduct);
                unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["Categories"] = GetCategories();
                ViewBag.Title = "Edit Product " + addProduct.Name;
                return View(addProduct);
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Delete(int id)
        {
            productManager.Delete(id);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private List<SelectListItem > GetCategories()
        {
          return   categoryManeger.GetList().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ID.ToString(),
            }).ToList();
        }



    }
}
