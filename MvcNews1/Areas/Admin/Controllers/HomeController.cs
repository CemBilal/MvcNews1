using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcNews1.Areas.Admin.Controllers
{
   [Area("Admin")]//burasi neden?
   [Authorize]//sayfadaki log in i tamamlamadan 
    //sayfaya girilmesini onler
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
