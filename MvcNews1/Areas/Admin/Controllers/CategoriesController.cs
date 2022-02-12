using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcNews1.Models;

namespace MvcNews1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext context;

        public CategoriesController(AppDbContext context)
        { 
         this.context = context;   
        }

        public IActionResult Index()
        {
            var model=context.Categories.Include((p)=>p.Posts).ToList();//TODO:INCLUDE NE ICIN??????
            return View(model);
        }

        public IActionResult Create ()
        {
            
            return View();
        }
        
        [HttpPost]
        public IActionResult Create (Category model)
        {
            context.Categories.Add(model);//kullanicinin girdigi verileri database a yazdi
            context.SaveChanges();//girilen verileri db ye kaydetti
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var model=context.Categories.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            context.Categories.Update(model);//kullanicinin girdigi verileri database a yazdi
            context.SaveChanges();//girilen verileri db ye kaydetti
            return RedirectToAction("Index");

        }
        //TODO:SWEET ALERT CALISMIYOR
        public IActionResult Delete(int id)
        {
            var model = context.Categories.Find(id);
            context.Categories.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index");
            return View(model);
        }
    }
}
