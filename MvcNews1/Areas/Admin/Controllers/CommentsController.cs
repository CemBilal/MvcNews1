using Microsoft.AspNetCore.Mvc;

namespace MvcNews1.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
