using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcNews1.Models;

namespace MvcNews1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators,Authors")]
    public class ArticlesController : Controller
    {
        private readonly AppDbContext context;

        public ArticlesController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var model = context.Posts.Where((p) => p.IsArticle).ToList();//TODO:INCLUDE NE ICIN??????
            return View(model);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Post model)
        {
            context.Posts.Add(model);//kullanicinin girdigi verileri database a yazdi
            context.SaveChanges();//girilen verileri db ye kaydetti
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var model = context.Posts.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Post model)
        {
            context.Posts.Update(model);//kullanicinin girdigi verileri database a yazdi
            context.SaveChanges();//girilen verileri db ye kaydetti
            return RedirectToAction("Index");

        }
        //TODO:SWEET ALERT CALISMIYOR
        public IActionResult Delete(int id)
        {
            var model = context.Posts.Find(id);
            context.Posts.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index");
            return View(model);
        }
    }
}
