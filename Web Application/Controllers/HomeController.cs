using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Web_Application.Controllers
{

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            dynamic data = new ExpandoObject();
            data.img = "6.jpg";
            data.head = "Our Date";
            return View(data);

            //return View("PendingPage");
        }
        [Route("contactus")]
        public IActionResult PendingPage()
        {
            return View();
        }

        //public ViewResult ContactU
    }
}
