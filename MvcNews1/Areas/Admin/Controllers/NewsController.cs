using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcNews1.Models;
using System.Security.Claims;

namespace MvcNews1.Areas.Admin.Controllers
{
    
    
        [Area("Admin")]
        [Authorize(Roles = "Administrators,Reporters")]
        
       public class NewsController : Controller

       { 
           private readonly AppDbContext context;

            public NewsController(AppDbContext context)
            {
                this.context = context;
            }


            public IActionResult Index()
            {
                var model = context.Posts.Where((p) => !p.IsArticle).ToList();//TODO:INCLUDE NE ICIN??????
            return View(model);
            }

            public IActionResult Create()
            {

                return View();
            }

            [HttpPost]
            public IActionResult Create(Post model)
            {
                model.IsArticle = false;
                model.Date=DateTime.Now;
                model.UserId= Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.Hit = 0;
                model.User=context.Users.FirstOrDefault(u => u.Id==model.UserId);
                model.Category=context.Categories.Find(u => u.Id==model.Category);
                //model.Category = context.Categories.ToList(Category);//TODO:HATAAAAAA!!!!!
                //FOTOGRAF YUKLEME SIKINTI
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
